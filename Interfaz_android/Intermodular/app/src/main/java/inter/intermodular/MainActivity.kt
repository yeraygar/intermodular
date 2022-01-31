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
import inter.intermodular.ui.theme.IntermodularTheme
import inter.intermodular.view_models.ClientViewModel


class MainActivity : ComponentActivity() {

    private val userViewModel by viewModels<UserViewModel>()
    private val clientViewModel by viewModels<ClientViewModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        Logger.addLogAdapter(AndroidLogAdapter())

        setContent {

            IntermodularTheme {

                //userViewModel.getClientAdmins()
                val navController = rememberNavController()

                NavHost(navController = navController, startDestination = ScreenNav.LoginScreen.route){

                                    /**LOGIN SCREEN*/
                    composable(
                        route = ScreenNav.LoginScreen.route
                    ){
                        Login(navController = navController, clientViewModel = clientViewModel)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el LOGIN", Toast.LENGTH_SHORT).show()
                        }
                    }

                                    /**VALIDATE LOGIN POPUP*/
                    composable(
                        route = "${ScreenNav.ValidateLoginScreen.route}",
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" //aqui no hace falta, nullable tampoco
                                nullable = true
                            } ,
                            navArgument("password"){
                                type = NavType.StringType
                                defaultValue = "Error"
                            }
                        )
                    ){ entry ->
                        entry.arguments?.getString("password")?.let { ValidateLoginScreen(email = entry.arguments?.getString("email")!!, password = it, clientViewModel = clientViewModel, navController = navController) }
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }

                                    /** MAIN SCREEN */
                    composable(
                        route = "${ScreenNav.MainScreen.route}/{email}",
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" //aqui no hace falta, nullable tampoco
                                nullable = true
                            }
                        )
                    ){ entry ->
                        MainScreen(email = entry.arguments?.getString("email"), clientViewModel = clientViewModel)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                                /** REGISTER SCREEN */
                    composable(
                        route = ScreenNav.RegisterScreen.route
                    ){
                        BackHandler(false) {
                            // TODO NO SE MUESTRA! (hay que pasarle el contexto de Login)
                            Toast.makeText(applicationContext, "BackButton Habilitado en Register", Toast.LENGTH_SHORT).show()
                        }
                        Register(navController = navController, clientViewModel = clientViewModel)
                    }

                                /** VALIDATE REGISTER SCREEN */
                    composable(
                        route = "${ScreenNav.ValidateRegisterScreen.route}/{email}",
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" //aqui no hace falta, nullable tampoco
                                nullable = true
                            }
                        )
                    ){ entry ->
                        ValidateRegisterScreen(email = entry.arguments?.getString("email"), clientViewModel = clientViewModel, navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }
        }
    }
}



