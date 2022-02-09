package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevas families a la API
 */
data class FamilyPost (
    var name : String,
    var id_client : String
)