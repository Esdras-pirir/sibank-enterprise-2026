package Jar;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/tarjetas")
public class TarjetaController {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public static class EmitirRequest {
        public Long idCuenta;
        public Long idTipoTarjeta;
        public Long idUsuarioActor;
    }

    public static class BloquearRequest {
        public Long idTarjeta;
        public Long idUsuarioActor;
    }

    // GET http://localhost:8080/tarjetas
    // Lista todas las tarjetas con el cliente dueno, numero de cuenta y tipo
    @GetMapping
    public java.util.List<Map<String, Object>> listar() {
        String sql = "SELECT t.id_tarjeta, t.numero_tarjeta, t.fecha_emision, t.fecha_vencimiento, " +
                     "t.limite_credito, t.saldo_utilizado, t.estado, " +
                     "tt.nombre AS tipo_tarjeta, tt.marca, " +
                     "c.numero_cuenta, cl.nombres || ' ' || cl.apellidos AS cliente " +
                     "FROM Tarjetas t " +
                     "JOIN TipoTarjeta tt ON tt.id_tipotarjeta = t.id_tipotarjeta " +
                     "JOIN Cuentas c ON c.id_cuenta = t.id_cuenta " +
                     "JOIN Clientes cl ON cl.id_cliente = c.id_cliente " +
                     "ORDER BY t.id_tarjeta DESC";
        return jdbcTemplate.queryForList(sql);
    }

    // GET http://localhost:8080/tarjetas/tipos
    // Lista los tipos de tarjeta disponibles (para el combo del formulario)
    @GetMapping("/tipos")
    public java.util.List<Map<String, Object>> listarTipos() {
        String sql = "SELECT id_tipotarjeta, nombre, marca, limite_default FROM TipoTarjeta ORDER BY nombre";
        return jdbcTemplate.queryForList(sql);
    }

    // POST http://localhost:8080/tarjetas/emitir
    // Body: {"idCuenta":1000000,"idTipoTarjeta":1,"idUsuarioActor":1}
    @PostMapping("/emitir")
    public Map<String, Object> emitir(@RequestBody EmitirRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            Map<String, Object> resultado = jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_emitir_tarjeta(?,?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idCuenta);
                    cs.setLong(2, req.idTipoTarjeta);
                    cs.setLong(3, req.idUsuarioActor);
                    cs.registerOutParameter(4, java.sql.Types.NUMERIC);
                    cs.registerOutParameter(5, java.sql.Types.VARCHAR);
                    cs.execute();
                    Map<String, Object> r = new HashMap<>();
                    r.put("idTarjeta", cs.getLong(4));
                    r.put("numeroTarjeta", cs.getString(5));
                    return r;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("idTarjeta", resultado.get("idTarjeta"));
            respuesta.put("numeroTarjeta", resultado.get("numeroTarjeta"));
            respuesta.put("mensaje", "Tarjeta emitida correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/tarjetas/bloquear
    // Body: {"idTarjeta":1,"idUsuarioActor":1}
    @PostMapping("/bloquear")
    public Map<String, Object> bloquear(@RequestBody BloquearRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_bloquear_tarjeta(?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idTarjeta);
                    cs.setLong(2, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Tarjeta bloqueada correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }
}