package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevos Clientes a la API
 */
data class ClientPost(
    var name: String,
    var email: String,
    var passw: String
)