package inter.intermodular

/** Clase para almacenar las rutas, con y sin parametros */
sealed class ScreenNav(val route: String) {
    object LoginScreen : ScreenNav("login_screen")
    object MapScreen : ScreenNav("map_screen")
    object RegisterScreen : ScreenNav("register_screen")
    object ValidateRegisterScreen : ScreenNav("validate_register_screen")
    object ValidateLoginScreen : ScreenNav("validate_login_screen")
    object UserSelectionScreen : ScreenNav("user_selection_screen")
    object TableScreen : ScreenNav("table_screen")
    object ValidateUserScreen : ScreenNav("validate_user_screen")

    fun withArgs(vararg args: String) : String{
        return buildString{
            append(route)
            args.forEach { arg -> append("/$arg") }
        }
    }
}