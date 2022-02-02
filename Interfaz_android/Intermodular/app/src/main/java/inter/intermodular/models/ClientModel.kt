package inter.intermodular.models

data class ClientModel(
    var _id: String,
    var name: String,
    var email: String,
    var passw: String
)

data class ClientPost(
    var name: String,
    var email: String,
    var passw: String
)