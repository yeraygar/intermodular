package inter.intermodular

sealed class ScreenNav(val route: String) {
    object LoginScreen : ScreenNav("login_screen")
    object MainScreen : ScreenNav("main_screen")
    object RegisterScreen : ScreenNav("register_screen")
    object ValidateRegisterScreen : ScreenNav("validate_register_screen")
    object ValidateLoginScreen : ScreenNav("validate_login_screen")

    fun withArgs(vararg args: String) : String{
        return buildString{
            append(route)
            args.forEach { arg -> append("/$arg") }
        }
    }
}