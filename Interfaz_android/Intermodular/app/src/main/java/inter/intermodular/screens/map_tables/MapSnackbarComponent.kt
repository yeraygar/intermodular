package inter.intermodular.screens.map_tables

import android.content.Context
import android.widget.Toast
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.Card
import androidx.compose.material.Icon
import androidx.compose.material.SnackbarHostState
import androidx.compose.material.Text
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ExitToApp
import androidx.compose.material.icons.filled.Logout
import androidx.compose.material.icons.filled.Settings
import androidx.compose.runtime.Composable
import androidx.compose.runtime.currentCompositionLocalContext
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clipToBounds
import androidx.compose.ui.draw.shadow
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.support.currentUser
import inter.intermodular.support.isNewClient

@Composable
fun SnackBarContent(
    snackbarHostState: SnackbarHostState,
    navController: NavHostController
) {
    Card(
        shape = RoundedCornerShape(10.dp),
       // backgroundColor = Color.Transparent,
        //border = BorderStroke(2.dp, Color.White),

        modifier = Modifier
            .padding(16.dp)
            .wrapContentSize()
            .clipToBounds()
    ) {
        Column {

            if (currentUser.rol == "Admin") {
                Box(
                    Modifier
                        .background(
                            color = colorResource(id = R.color.azul_oscuro),
                            //RoundedCornerShape(15.dp)
                        )
                        .shadow(
                            elevation = 1.dp,
                            shape = RoundedCornerShape(1.dp),
                            clip = false
                        )
                        .clickable {
                            Logger.d("CLICK EN ADMIN")
                            //navController.navigate(ScreenNav.LoginScreen.route)
                            /**TODO Agregar ruta de opciones Admin si el currentUser rol es Admin*/
                            if(isNewClient){
                                //TODO MOSTRAR MENSAJE ESPECIAL CON EL ADMIN POR DEFECTO
                            }

                        }
                ) {
                    Column(
                        modifier = Modifier
                            .padding(8.dp)
                            .fillMaxWidth()
                            .height(150.dp),
                        verticalArrangement = Arrangement.Center,
                        horizontalAlignment = Alignment.CenterHorizontally
                    ) {
                        Icon(imageVector = Icons.Default.Settings, contentDescription = "")
                        Text(text = "Admin Options")
                    }
                }

                Spacer(modifier = Modifier.height(10.dp))
            }

            Box(
                Modifier
                    .background(
                        color = colorResource(id = R.color.azul),
                       // RoundedCornerShape(15.dp)
                    )
                    .shadow(
                        elevation = 1.dp,
                        shape = RoundedCornerShape(1.dp),
                        clip = false
                    )
                    .clickable {
                        Logger.d("CLICK EN CERRAR")
                        snackbarHostState.currentSnackbarData?.dismiss()
                    }
            ) {
                Column(
                    modifier = Modifier
                        .padding(8.dp)
                        .fillMaxWidth()
                        .height(150.dp),
                    verticalArrangement = Arrangement.Center,
                    horizontalAlignment = Alignment.CenterHorizontally
                ) {
                    Icon(imageVector = Icons.Default.ExitToApp, contentDescription = "")
                    Text(text = "Cerrar")
                }
            }

            Spacer(modifier = Modifier.height(10.dp))

            Box(
                Modifier
                    .background(
                        color = colorResource(id = R.color.rojo),
                     //   RoundedCornerShape(15.dp)
                    )
                    .shadow(
                        elevation = 1.dp,
                        shape = RoundedCornerShape(1.dp),
                        clip = false
                    )
                    .clickable {
                        Logger.d("CLICK EN LOGOUT")
                        navController.navigate(ScreenNav.LoginScreen.route)

                    }

            ) {
                Column(
                    modifier = Modifier
                        .padding(8.dp)
                        .fillMaxWidth()
                        .height(150.dp),
                    horizontalAlignment = Alignment.CenterHorizontally,
                    verticalArrangement = Arrangement.Center,

                    ) {
                    Icon(imageVector = Icons.Default.Logout, contentDescription = "")
                    Text(text = "LogOut")
                }
            }
        }
    }
}