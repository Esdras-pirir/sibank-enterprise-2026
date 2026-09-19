package Jar;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.*;

import java.sql.Types;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/usuarios")
public class UsuarioController {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public static class LoginRequest {
        public String username;
        public String password;
    }

    public static class RegistrarRequest {
        public String username;
        public String password;
        public String correo;
        public Long idRol;
        public Long idUsuarioActor;
    }

    public static class CambiarEstadoRequest {
        public Long idUsuario;
        public String nuevoEstado; // "S" o "N"
        public Long idUsuarioActor;
    }

    public static class ResetPasswordRequest {
        public Long idUsuario;
        public String passwordNuevo;
        public Long idUsuarioActor;
    }

    // GET http://localhost:8080/usuarios
    // Lista todos los usuarios con el nombre de su rol
    @GetMapping
    public java.util.List<Map<String, Object>> listar() {
        String sql = "SELECT u.id_usuario, u.username, u.correo, r.nombre AS rol, " +
                     "u.activo, u.intentos_fallidos, u.fecha_creacion, u.ultimo_login " +
                     "FROM Usuarios u " +
                     "JOIN Roles r ON r.id_rol = u.id_rol " +
                     "ORDER BY u.id_usuario DESC";
        return jdbcTemplate.queryForList(sql);
    }

    // GET http://localhost:8080/usuarios/roles
    // Lista los roles disponibles (para el combo del formulario)
    @GetMapping("/roles")
    public java.util.List<Map<String, Object>> listarRoles() {
        String sql = "SELECT id_rol, nombre, descripcion FROM Roles ORDER BY nombre";
        return jdbcTemplate.queryForList(sql);
    }

    // POST http://localhost:8080/usuarios/registrar
    // Body: {"username":"jperez","password":"Clave123!","correo":"j@correo.com","idRol":2,"idUsuarioActor":1}
    @PostMapping("/registrar")
    public Map<String, Object> registrar(@RequestBody RegistrarRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            Long idGenerado = jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_registrar_usuario(?,?,?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setString(1, req.username);
                    cs.setString(2, req.password);
                    cs.setString(3, req.correo);
                    cs.setLong(4, req.idRol);
                    cs.setLong(5, req.idUsuarioActor);
                    cs.registerOutParameter(6, Types.NUMERIC);
                    cs.execute();
                    return cs.getLong(6);
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("idUsuario", idGenerado);
            respuesta.put("mensaje", "Usuario registrado correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/usuarios/cambiar-estado
    // Body: {"idUsuario":2,"nuevoEstado":"N","idUsuarioActor":1}
    @PostMapping("/cambiar-estado")
    public Map<String, Object> cambiarEstado(@RequestBody CambiarEstadoRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_cambiar_estado_usuario(?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idUsuario);
                    cs.setString(2, req.nuevoEstado);
                    cs.setLong(3, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Estado del usuario actualizado.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/usuarios/resetear-password
    // Body: {"idUsuario":2,"passwordNuevo":"NuevaClave123!","idUsuarioActor":1}
    @PostMapping("/resetear-password")
    public Map<String, Object> resetearPassword(@RequestBody ResetPasswordRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_resetear_password(?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idUsuario);
                    cs.setString(2, req.passwordNuevo);
                    cs.setLong(3, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Contrasena reseteada correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/usuarios/login
    // Body: {"username":"admin","password":"Admin123!"}
    @PostMapping("/login")
    public Map<String, Object> login(@RequestBody LoginRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            Map<String, Object> resultado = jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_login_usuario(?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setString(1, req.username);
                    cs.setString(2, req.password);
                    cs.registerOutParameter(3, Types.NUMERIC);  // id_usuario_out
                    cs.registerOutParameter(4, Types.VARCHAR);  // mensaje_out
                    cs.execute();

                    Map<String, Object> r = new HashMap<>();
                    Object idUsuario = cs.getObject(3);
                    r.put("idUsuario", idUsuario);
                    r.put("mensaje", cs.getString(4));
                    return r;
                }
            );

            boolean exito = resultado.get("idUsuario") != null;
            respuesta.put("estado", exito ? "OK" : "ERROR");
            respuesta.putAll(resultado);

            // Si el login fue exitoso, buscamos si esta vinculado a un cliente
            // (para saber si es un usuario del portal web y de que cliente es)
            if (exito) {
                Long idUsuario = ((Number) resultado.get("idUsuario")).longValue();
                String sqlCliente = "SELECT u.id_cliente, c.nombres || ' ' || c.apellidos AS nombre_cliente " +
                                     "FROM Usuarios u LEFT JOIN Clientes c ON c.id_cliente = u.id_cliente " +
                                     "WHERE u.id_usuario = ?";
                Map<String, Object> datosCliente = jdbcTemplate.queryForMap(sqlCliente, idUsuario);
                respuesta.put("idCliente", datosCliente.get("ID_CLIENTE"));
                respuesta.put("nombreCliente", datosCliente.get("NOMBRE_CLIENTE"));
            }
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }
}