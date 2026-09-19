SELECT u.id_usuario, u.username, u.id_cliente, r.nombre AS rol, c.nombres, c.apellidos
FROM Usuarios u
JOIN Roles r ON r.id_rol = u.id_rol
LEFT JOIN Clientes c ON c.id_cliente = u.id_cliente
WHERE u.username = 'juanperez';