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
