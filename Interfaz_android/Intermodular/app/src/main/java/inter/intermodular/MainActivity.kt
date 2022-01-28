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
import inter.intermodular.screens.Login
import inter.intermodular.screens.MainScreen
import inter.intermodular.screens.Register
import inter.intermodular.ui.theme.IntermodularTheme


class MainActivity : ComponentActivity() {

    private val userViewModel by viewModels<UserViewModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        Logger.addLogAdapter(AndroidLogAdapter())

        setContent {

            IntermodularTheme {

                userViewModel.getClientAdmins()
                val navController = rememberNavController()

                NavHost(navController = navController, startDestination = ScreenNav.LoginScreen.route){
                    composable(
                        route = ScreenNav.LoginScreen.route
                    ){
                        Login(navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el LOGIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                    composable(
                        route = "${ScreenNav.MainScreen.route}/{email}", //optional arguments ?name={name}, +args /{name}/{age}
                        arguments = listOf(
                            navArgument("email") {
                                type = NavType.StringType
                                defaultValue = "Email" //aqui no hace falta, nullable tampoco
                                nullable = true
                            }
                        )
                    ){ entry ->
                        MainScreen(name = entry.arguments?.getString("email"), userViewModel = userViewModel)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                    composable(
                        route = ScreenNav.RegisterScreen.route
                    ){
                        BackHandler(false) {
                            Toast.makeText(applicationContext, "BackButton Habilitado en Register", Toast.LENGTH_SHORT).show() // TODO NO SE MUESTRA!
                        }
                        Register(navController = navController)
                    }
                }
            }
        }
    }
}



