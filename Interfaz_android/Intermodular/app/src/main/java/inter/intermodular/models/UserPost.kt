package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevos Usuarios a la API
 */
data class UserPost(
    var name: String,
    var passw: String,
    var id_client: String,
    var rol: String,
)
