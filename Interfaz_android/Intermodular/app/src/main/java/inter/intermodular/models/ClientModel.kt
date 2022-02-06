package inter.intermodular.models

/**
 * Modelo usado para recibir
 * las respuestas de la API
 */
data class ClientModel(
    var _id: String,
    var name: String,
    var email: String,
    var passw: String
)

/**
 * Modelo usado para mandar
 * nuevos Clientes a la API
 */
data class ClientPost(
    var name: String,
    var email: String,
    var passw: String
)