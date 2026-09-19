package Jar;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

@RestController
@RequestMapping("/bitacora")
public class BitacoraController {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    // GET http://localhost:8080/bitacora
    // Lista las ultimas 200 acciones registradas, con el nombre del usuario que las hizo
    @GetMapping
    public java.util.List<Map<String, Object>> listar() {
        String sql = "SELECT b.id_bitacora, u.username, b.accion, b.modulo, b.detalle, b.fecha " +
                     "FROM Bitacora b " +
                     "LEFT JOIN Usuarios u ON u.id_usuario = b.id_usuario " +
                     "ORDER BY b.fecha DESC " +
                     "FETCH FIRST 200 ROWS ONLY";
        return jdbcTemplate.queryForList(sql);
    }
}