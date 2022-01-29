package inter.intermodular

import androidx.activity.compose.setContent
import androidx.activity.viewModels
import android.os.Bundle
import android.widget.Toast
import androidx.activity.ComponentActivity
import androidx.activity.compose.BackHandler
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.Button
import androidx.compose.material.ButtonDefaults
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument
import inter.intermodular.view_models.UserViewModel
import com.orhanobut.logger.AndroidLogAdapter
import com.orhanobut.logger.Logger
import inter.intermodular.screens.Login
import inter.intermodular.screens.MainScreen
import inter.intermodular.screens.Register
import inter.intermodular.screens.ValidateLoginScreen
import inter.intermodular.ui.theme.IntermodularTheme
import inter.intermodular.ui.theme.Purple500
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


                    composable(
                        route = ScreenNav.LoginScreen.route
                    ){
                        Login(navController = navController, clientViewModel = clientViewModel)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el LOGIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                    composable(
                        route = "${ScreenNav.ValidateLoginScreen.route}/{email}",
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" //aqui no hace falta, nullable tampoco
                                nullable = true
                            }
                        )
                    ){ entry ->
                        ValidateLoginScreen(email = entry.arguments?.getString("email"), clientViewModel = clientViewModel, navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }

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

                    composable(
                        route = ScreenNav.RegisterScreen.route
                    ){
                        BackHandler(false) {
                            // TODO NO SE MUESTRA! (hay que pasarle el contexto de Login)
                            Toast.makeText(applicationContext, "BackButton Habilitado en Register", Toast.LENGTH_SHORT).show()
                        }
                        Register(navController = navController)
                    }
                }
            }
        }
    }


}



