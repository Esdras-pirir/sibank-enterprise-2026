package Jar;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.*;

import java.math.BigDecimal;
import java.sql.Types;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/cuentas")
public class TestController {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    // GET http://localhost:8080/cuentas/test-conexion
    @GetMapping("/test-conexion")
    public String testConexion() {
        try {
            Integer resultado = jdbcTemplate.queryForObject("SELECT 1 FROM DUAL", Integer.class);
            return "Conexion exitosa con Oracle. Resultado: " + resultado;
        } catch (Exception e) {
            return "ERROR de conexion: " + e.getMessage();
        }
    }

    // GET http://localhost:8080/cuentas/saldo/1000001
    @GetMapping("/saldo/{idCuenta}")
    public Map<String, Object> obtenerSaldo(@PathVariable Long idCuenta) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            BigDecimal saldo = jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ ? = call fn_obtener_saldo(?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.registerOutParameter(1, Types.NUMERIC);
                    cs.setLong(2, idCuenta);
                    cs.execute();
                    return cs.getBigDecimal(1);
                }
            );
            respuesta.put("idCuenta", idCuenta);
            respuesta.put("saldo", saldo);
            respuesta.put("estado", "OK");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    public static class MontoRequest {
        public Long idCuenta;
        public BigDecimal monto;
        public String descripcion;
        public Long idUsuarioActor;
    }

    public static class TransferenciaRequest {
        public Long idCuentaOrigen;
        public Long idCuentaDestino;
        public BigDecimal monto;
        public Long idUsuarioActor;
    }

    public static class AbrirCuentaRequest {
        public Long idCliente;
        public Long idTipoCuenta;
        public Long idSucursal;
        public BigDecimal saldoInicial;
        public Long idUsuarioActor;
    }

    // POST http://localhost:8080/cuentas/depositar
    @PostMapping("/depositar")
    public Map<String, Object> depositar(@RequestBody MontoRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_depositar(?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idCuenta);
                    cs.setBigDecimal(2, req.monto);
                    cs.setString(3, req.descripcion);
                    cs.setLong(4, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Deposito realizado correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/cuentas/retirar
    @PostMapping("/retirar")
    public Map<String, Object> retirar(@RequestBody MontoRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_retirar(?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idCuenta);
                    cs.setBigDecimal(2, req.monto);
                    cs.setString(3, req.descripcion);
                    cs.setLong(4, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Retiro realizado correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/cuentas/transferir
    @PostMapping("/transferir")
    public Map<String, Object> transferir(@RequestBody TransferenciaRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_transferir(?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idCuentaOrigen);
                    cs.setLong(2, req.idCuentaDestino);
                    cs.setBigDecimal(3, req.monto);
                    cs.setLong(4, req.idUsuarioActor);
                    cs.execute();
                    return null;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.put("mensaje", "Transferencia realizada correctamente.");
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // POST http://localhost:8080/cuentas/abrir
    // Body: {"idCliente":1,"idTipoCuenta":1,"idSucursal":1,"saldoInicial":100,"idUsuarioActor":1}
    @PostMapping("/abrir")
    public Map<String, Object> abrirCuenta(@RequestBody AbrirCuentaRequest req) {
        Map<String, Object> respuesta = new HashMap<>();
        try {
            Map<String, Object> resultado = jdbcTemplate.execute(
                (java.sql.Connection con) -> con.prepareCall("{ call sp_abrir_cuenta(?,?,?,?,?,?,?) }"),
                (java.sql.CallableStatement cs) -> {
                    cs.setLong(1, req.idCliente);
                    cs.setLong(2, req.idTipoCuenta);
                    cs.setLong(3, req.idSucursal);
                    cs.setBigDecimal(4, req.saldoInicial);
                    cs.setLong(5, req.idUsuarioActor);
                    cs.registerOutParameter(6, Types.NUMERIC); // id_cuenta_out
                    cs.registerOutParameter(7, Types.VARCHAR); // numero_cuenta_out
                    cs.execute();

                    Map<String, Object> r = new HashMap<>();
                    r.put("idCuenta", cs.getLong(6));
                    r.put("numeroCuenta", cs.getString(7));
                    return r;
                }
            );
            respuesta.put("estado", "OK");
            respuesta.putAll(resultado);
        } catch (Exception e) {
            respuesta.put("estado", "ERROR");
            respuesta.put("mensaje", e.getMessage());
        }
        return respuesta;
    }

    // GET http://localhost:8080/cuentas
    // Lista todas las cuentas con el nombre del cliente dueno y el tipo de cuenta
    @GetMapping
    public java.util.List<Map<String, Object>> listarCuentas() {
        String sql = "SELECT c.id_cuenta, c.numero_cuenta, c.saldo, c.estado, " +
                     "c.fecha_apertura, cl.id_cliente, " +
                     "cl.nombres || ' ' || cl.apellidos AS cliente, tc.nombre AS tipo_cuenta " +
                     "FROM Cuentas c " +
                     "JOIN Clientes cl ON cl.id_cliente = c.id_cliente " +
                     "JOIN TipoCuenta tc ON tc.id_tipocuenta = c.id_tipocuenta " +
                     "ORDER BY c.id_cuenta DESC";
        return jdbcTemplate.queryForList(sql);
    }

    // GET http://localhost:8080/cuentas/tipos
    // Lista los tipos de cuenta disponibles (para el combo del formulario "Abrir cuenta")
    @GetMapping("/tipos")
    public java.util.List<Map<String, Object>> listarTiposCuenta() {
        String sql = "SELECT id_tipocuenta, nombre, tasa_interes, monto_minimo FROM TipoCuenta ORDER BY nombre";
        return jdbcTemplate.queryForList(sql);
    }

    // GET http://localhost:8080/cuentas/sucursales
    // Lista las sucursales disponibles (para el combo del formulario "Abrir cuenta")
    @GetMapping("/sucursales")
    public java.util.List<Map<String, Object>> listarSucursales() {
        String sql = "SELECT id_sucursal, nombre FROM Sucursales ORDER BY nombre";
        return jdbcTemplate.queryForList(sql);
    }

    // GET http://localhost:8080/cuentas/movimientos/{idCuenta}
    // Lista los ultimos 50 movimientos (transacciones) de una cuenta especifica
    @GetMapping("/movimientos/{idCuenta}")
    public java.util.List<Map<String, Object>> listarMovimientos(@PathVariable Long idCuenta) {
        String sql = "SELECT t.id_transaccion, tm.nombre AS tipo_movimiento, " +
                     "t.monto, t.saldo_nuevo, t.descripcion, t.fecha " +
                     "FROM Transacciones t " +
                     "JOIN TipoMovimiento tm ON tm.id_tipomovimiento = t.id_tipomovimiento " +
                     "WHERE t.id_cuenta = ? " +
                     "ORDER BY t.fecha DESC " +
                     "FETCH FIRST 50 ROWS ONLY";
        return jdbcTemplate.queryForList(sql, idCuenta);
    }

    // GET http://localhost:8080/cuentas/cliente/{idCliente}
    // Lista solo las cuentas de un cliente especifico (para el portal web)
    @GetMapping("/cliente/{idCliente}")
    public java.util.List<Map<String, Object>> listarCuentasPorCliente(@PathVariable Long idCliente) {
        String sql = "SELECT c.id_cuenta, c.numero_cuenta, c.saldo, c.estado, " +
                     "c.fecha_apertura, tc.nombre AS tipo_cuenta " +
                     "FROM Cuentas c " +
                     "JOIN TipoCuenta tc ON tc.id_tipocuenta = c.id_tipocuenta " +
                     "WHERE c.id_cliente = ? " +
                     "ORDER BY c.id_cuenta";
        return jdbcTemplate.queryForList(sql, idCliente);
    }
}