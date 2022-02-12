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
import inter.intermodular.view_models.MapViewModel
import com.orhanobut.logger.AndroidLogAdapter
import com.orhanobut.logger.Logger
import inter.intermodular.screens.login_register.*
import inter.intermodular.screens.map_tables.MapScreen
import inter.intermodular.screens.table_payment.TableScreen
import inter.intermodular.ui.theme.IntermodularTheme
import inter.intermodular.view_models.LoginRegisterViewModel
import inter.intermodular.view_models.TableViewModel

/**********************************************************************************************************
 * FUNCIONAMIENTO GENERAL:                                                                                *
 **********************************************************************************************************
 *                                                                                                        *
 * La unica actividad real es esta, la MainActivity, el resto de vistas seran como fragments y            *
 * viajaremos entre ellos por medio del NavHost donde especificaremos cada una de las rutas que           *
 * mantenemos separadas en el ScreenNav, para viajar a cada una de las vistas o screens debemos           *
 * pasar el navControler como parametro para poder seguir viajando dentro de la aplicacion, y,            *
 * por otro lado, le pasaremos el ViewModel que necesite cada una de las vistas ya que solo lo            *
 * podremos instanciar antes del onCreate de la MainActivity.                                             *
 *                                                                                                        *
 * ViewModel                                                                                              *
 * Contiene toda la funcionalidad que requiera la screen o un grupo de ellas, por ejemplo, el paquete     *
 * login_register contiene las screens que requieren del LoginRegisterViewModel, es la unica manera       *
 * que tenemos de llamar a una funcion suspendible dentro de un Composable. Estas funciones suspendibles  *
 * seran las que instancien el companionObject que contiene Retrofit de la interfaz ApiServices, a su vez *
 * esta instancia nos provee de las rutas de CRUD para llamar a la API.                                   *
 *                                                                                                        *
 * Por ultimo estan los models que solo son un recipiente de atributos que coinciden con el objeto        *
 * creado en la API, los usamos para almacenar datos, hacer peticiones y recibir respuestas.              *
 *                                                                                                        *
 **********************************************************************************************************/
class MainActivity : ComponentActivity() {

    private val mapViewModel by viewModels<MapViewModel>()
    private val loginRegisterViewModel by viewModels<LoginRegisterViewModel>()
    private val tableViewModel by viewModels<TableViewModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        Logger.addLogAdapter(AndroidLogAdapter())

        val activityKiller : () -> Unit = {this.finish()}

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
                        ValidateLoginScreen(loginRegisterViewModel = loginRegisterViewModel, navController = navController, activityKiller = activityKiller)
                        BackHandler(false) {
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
                        BackHandler(false) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAIN", Toast.LENGTH_SHORT).show()
                        }
                    }
                                    /*** USER SELECTION SCREEN ***/
                    composable(
                        route = ScreenNav.UserSelectionScreen.route
                    ){
                        UserSelection(loginRegisterViewModel = loginRegisterViewModel, navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAP", Toast.LENGTH_SHORT).show()
                        }
                    }
/*
                                    *//*** USER VALIDATION SCREEN ***//*
                    composable(
                        route = ScreenNav.ValidateUserScreen.route
                    ){
                        ValidateUser(loginRegisterViewModel = loginRegisterViewModel, navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAP", Toast.LENGTH_SHORT).show()
                        }
                    }*/

                                    /*** MAP SCREEN ***/
                    composable(
                        route = ScreenNav.MapScreen.route
                    ){
                        MapScreen(mapViewModel = mapViewModel, navController = navController)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado en el MAP", Toast.LENGTH_SHORT).show()
                        }
                    }

                                    /*** TABLE SCREEN ***/
                    composable(
                        route = ScreenNav.TableScreen.route
                    ){
                        TableScreen(navController = navController, tableViewModel = tableViewModel, applicationContext)
                        BackHandler(true) {
                            Toast.makeText(applicationContext, "BackButton Deshabilitado por el momento", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }
        }
    }
}



