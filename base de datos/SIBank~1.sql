SELECT object_name, status FROM user_objects 
WHERE object_type = 'PROCEDURE' 
AND object_name IN ('SP_REGISTRAR_USUARIO', 'SP_CAMBIAR_ESTADO_USUARIO', 'SP_RESETEAR_PASSWORD');