package inter.intermodular

import androidx.activity.compose.setContent
import androidx.activity.viewModels
import android.os.Bundle
import android.widget.Toast
import androidx.activity.ComponentActivity
import androidx.activity.compose.BackHandler
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument
import inter.intermodular.view_models.UserViewModel
import com.orhanobut.logger.AndroidLogAdapter
import com.orhanobut.logger.Logger
import inter.intermodular.screens.*
import inter.intermodular.screens.login_register.Login
import inter.intermodular.screens.login_register.Register
import inter.intermodular.screens.login_register.ValidateLoginScreen
import inter.intermodular.screens.login_register.ValidateRegisterScreen
import inter.intermodular.ui.theme.IntermodularTheme
import inter.intermodular.view_models.LoginRegisterViewModel


class MainActivity : ComponentActivity() {

    private val userViewModel by viewModels<UserViewModel>()
    private val loginRegisterViewModel by viewModels<LoginRegisterViewModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        Logger.addLogAdapter(AndroidLogAdapter())

        setContent {

            IntermodularTheme {

                val navController = rememberNavController()

                NavHost(navController = navController, startDestination = ScreenNav.LoginScreen.route){

                                    /*** LOGIN SCREEN ***/
                    composable(
                        route = ScreenNav.LoginScreen.route
                    ){
                        Login(navController = navController, loginRegisterViewModel = loginRegisterViewModel)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el LOGIN", Toast.LENGTH_SHORT).show()
                        }
                    }

                                    /*** VALIDATE LOGIN SCREEN ***/
                    composable(
                        route = ScreenNav.ValidateLoginScreen.route,
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" // no hace falta, nullable tampoco
                                nullable = true
                            } ,
                            navArgument("password"){
                                type = NavType.StringType
                                defaultValue = "Error"
                            }
                        )
                    ){
                        ValidateLoginScreen(loginRegisterViewModel = loginRegisterViewModel, navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }

                                    /*** REGISTER SCREEN ***/
                    composable(
                        route = ScreenNav.RegisterScreen.route
                    ){
                        Register(navController = navController, loginRegisterViewModel = loginRegisterViewModel)
                    }

                                    /*** VALIDATE REGISTER SCREEN ***/
                    composable(
                        route = "${ScreenNav.ValidateRegisterScreen.route}/{name}/{email}/{password}",
                        arguments = listOf(
                            navArgument("name") {
                                type = NavType.StringType
                                defaultValue = "Name" // no hace falta, nullable tampoco
                                nullable = true
                            },
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" // no hace falta, nullable tampoco
                                nullable = true
                            },
                            navArgument("password") {
                                type = NavType.StringType
                                defaultValue = "password" // no hace falta, nullable tampoco
                                nullable = true
                            }
                        )
                    ){ entry ->
                        ValidateRegisterScreen(
                            name = entry.arguments?.getString("name"),
                            email = entry.arguments?.getString("email"),
                            password = entry.arguments?.getString("password"),
                            loginRegisterViewModel = loginRegisterViewModel,
                            navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }

                                    /*** MAIN SCREEN ***/
                    composable(
                        route = "${ScreenNav.MainScreen.route}/{email}",
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" //no hace falta, nullable tampoco
                                nullable = true
                            }
                        )
                    ){ entry ->
                        MainScreen(email = entry.arguments?.getString("email"), loginRegisterViewModel = loginRegisterViewModel)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }
        }
    }
}



