package Jar;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.*;

import java.sql.Date;
import java.sql.Types;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/clientes")
public class ClienteController {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public static class ClienteRequest {
        public String nombres;
        public String apellidos;
        public String dpi;
        public String nit;
        public String fechaNacimiento; // formato "YYYY-MM-DD"
        public String telefono;
        public String correo;
        public Long idUsuarioActor;
    }

    public static class ClienteEditRequest {
        public Long idCliente;
        public String telefono;
        public String correo;
        public Long idUsuarioActor;
    }

    // GET http://localhost:8080/clientes
    // Devuelve la lista de clientes activos e inactivos (para la tabla del Dashboard)
    @GetMapping
    public java.util.List<Map<String, Object>> listar() {
        String sql = "SELECT id_cliente, nombres, apellidos, dpi, nit, " +
                     "telefono, correo, estado, fecha_registro " +
                     "FROM Clientes ORDER BY id_cliente DESC";
        return jdbcTemplate.queryForList(sql);
    }

    // DELETE http://localhost:8080/clientes/{id}?idUsuarioActor=1
    // Baja logica del cliente (llama a sp_eliminar_cliente)
    @DeleteMapping("/{id}")
    public Map<String, Object> eliminar(@PathVariable Long id, @RequestParam Long idUsuarioActor) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_eliminar_cliente(?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, id);
                    cs.setLong(2, idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Cliente dado de baja correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/clientes
    // Body: {"nombres":"Maria","apellidos":"Lopez","dpi":"9998887776665",
    //        "nit":"555444-3","fechaNacimiento":"1998-03-20",
    //        "telefono":"55511122","correo":"maria@correo.com","idUsuarioActor":1}
    @PostMapping
    public Map<String, Object> registrar(@RequestBody ClienteRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            Long idClienteGenerado = jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_registrar_cliente(?,?,?,?,?,?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setString(1, req.nombres);
                    cs.setString(2, req.apellidos);
                    cs.setString(3, req.dpi);
                    cs.setString(4, req.nit);
                    cs.setDate(5, Date.valueOf(req.fechaNacimiento));
                    cs.setString(6, req.telefono);
                    cs.setString(7, req.correo);
                    cs.setLong(8, req.idUsuarioActor);
                    cs.registerOutParameter(9, Types.NUMERIC); // id_cliente_out
                    cs.execute();
                    return cs.getLong(9);
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("idCliente", idClienteGenerado);
            respuesta.put("mensaje", "Cliente registrado correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // PUT http://localhost:8080/clientes
    // Body: {"idCliente":1,"telefono":"55599999","correo":"nuevo@correo.com","idUsuarioActor":1}
    @PutMapping
    public Map<String, Object> editar(@RequestBody ClienteEditRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_editar_cliente(?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idCliente);
                    cs.setString(2, req.telefono);
                    cs.setString(3, req.correo);
                    cs.setLong(4, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Cliente actualizado correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }
}