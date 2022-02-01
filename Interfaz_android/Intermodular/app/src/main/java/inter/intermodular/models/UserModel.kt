package inter.intermodular.models

data class UserModel(
    var _id: String,
    var name: String,
    var passw: String,
    var id_client: String,
    var rol: String,
    var active: Boolean
)
