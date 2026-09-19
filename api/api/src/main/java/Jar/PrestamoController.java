package Jar;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/prestamos")
public class PrestamoController {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public static class AprobarRequest {
        public Long idPrestamo;
        public String aprobar;      // "S" o "N"
        public Long idEmpleado;
        public Long idUsuarioActor;
    }

    // POST http://localhost:8080/prestamos/aprobar
    // Body: {"idPrestamo":1,"aprobar":"S","idEmpleado":1,"idUsuarioActor":1}
    @PostMapping("/aprobar")
    public Map<String, Object> aprobar(@RequestBody AprobarRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_aprobar_prestamo(?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idPrestamo);
                    cs.setString(2, req.aprobar);
                    cs.setLong(3, req.idEmpleado);
                    cs.setLong(4, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Solicitud de prestamo procesada correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }
         public static class SolicitudRequest {
    public Long idCliente;
    public Long idTipoPrestamo;
    public Long idCuentaDesembolso;
    public java.math.BigDecimal monto;
    public Integer plazoMeses;
    public Long idUsuarioActor;
    }
         // POST http://localhost:8080/prestamos/solicitar
// Body: {"idCliente":1,"idTipoPrestamo":1,"idCuentaDesembolso":1000000,
//        "monto":5000,"plazoMeses":12,"idUsuarioActor":1}
@PostMapping("/solicitar")
public Map<String, Object> solicitar(@RequestBody SolicitudRequest req) {
    Map<String, Object> respuesta = new HashMap<>();
    try {
        Long idPrestamoGenerado = jdbcTemplate.execute(
            (java.sql.Connection con) -> con.prepareCall("{ call sp_solicitar_prestamo(?,?,?,?,?,?,?) }"),
            (java.sql.CallableStatement cs) -> {
                cs.setLong(1, req.idCliente);
                cs.setLong(2, req.idTipoPrestamo);
                cs.setLong(3, req.idCuentaDesembolso);
                cs.setBigDecimal(4, req.monto);
                cs.setInt(5, req.plazoMeses);
                cs.setLong(6, req.idUsuarioActor);
                cs.registerOutParameter(7, java.sql.Types.NUMERIC);
                cs.execute();
                return cs.getLong(7);
            }
        );
        respuesta.put("estado", "OK");
        respuesta.put("idPrestamo", idPrestamoGenerado);
        respuesta.put("mensaje", "Solicitud de prestamo registrada correctamente.");
    } catch (Exception e) {
        respuesta.put("estado", "ERROR");
        respuesta.put("mensaje", e.getMessage());
    }
    return respuesta;
}

        // GET http://localhost:8080/prestamos
        // Lista todos los prestamos con el nombre del cliente y tipo
        @GetMapping
        public java.util.List<Map<String, Object>> listar() {
            String sql = "SELECT p.id_prestamo, cl.nombres || ' ' || cl.apellidos AS cliente, " +
                         "tp.nombre AS tipo_prestamo, p.monto, p.plazo_meses, p.tasa_interes, " +
                         "p.cuota_mensual, p.saldo_pendiente, p.estado, p.fecha_solicitud " +
                         "FROM Prestamos p " +
                         "JOIN Clientes cl ON cl.id_cliente = p.id_cliente " +
                         "JOIN TipoPrestamo tp ON tp.id_tipoprestamo = p.id_tipoprestamo " +
                         "ORDER BY p.id_prestamo DESC";
            return jdbcTemplate.queryForList(sql);
        }

        // GET http://localhost:8080/prestamos/tipos
        // Lista los tipos de prestamo disponibles (para el combo del formulario)
        @GetMapping("/tipos")
        public java.util.List<Map<String, Object>> listarTipos() {
            String sql = "SELECT id_tipoprestamo, nombre, tasa_interes_anual, plazo_max_meses " +
                         "FROM TipoPrestamo ORDER BY nombre";
            return jdbcTemplate.queryForList(sql);
        }
}