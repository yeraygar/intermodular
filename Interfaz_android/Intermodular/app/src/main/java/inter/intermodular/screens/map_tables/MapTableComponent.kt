package inter.intermodular.screens.map_tables

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.material.Button
import androidx.compose.material.ButtonDefaults
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavHostController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.support.currentTable
import inter.intermodular.support.currentZone
import inter.intermodular.support.currentZoneTables
import inter.intermodular.view_models.MapViewModel

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun MapTableComponent(mapViewModel: MapViewModel, navController: NavHostController) {
    LazyVerticalGrid(
        cells = GridCells.Fixed(6),
        modifier = Modifier
            .fillMaxSize()
            .padding(5.dp)
    ) {
        mapViewModel.getZoneTables(currentZone?._id ?: "Error")
        if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
            for (i in 0 until mapViewModel.zoneTablesResponse.count()) {
                item {
                    RowContent(i, mapViewModel, navController)
                }
            }
        }
    }
}

@Composable
fun RowContent(i: Int, mapViewModel: MapViewModel, navController: NavHostController) {

    if(currentZone != null){
        mapViewModel.getZoneTables(currentZone!!._id)
        if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
            currentZoneTables = mapViewModel.zoneTablesResponse

            var countTables = currentZoneTables.count()
            Logger.i("Numero de Mesas cargadas en la zona: ${countTables.toString()}")

            /**TODO Asignar la currentMesa a la i y navegar a la vista de la mesa*/
            Button(
                modifier = Modifier
                    .height(75.dp)
                    .width(75.dp)
                    .padding(3.dp),
                colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
                elevation = ButtonDefaults.elevation(
                    defaultElevation = 15.dp,
                    pressedElevation = 1.dp,
                    disabledElevation = 0.dp

                ),
                onClick = {
                    currentTable = mapViewModel.zoneTablesResponse[i]
                    Logger.i("Mesa seleccionada $currentTable")
                    navController.navigate(ScreenNav.TableScreen.route)
                }) {
                Text(text = currentZoneTables[i].name.substring(0,3), fontSize = 11.sp,
                )
            }
        }
    }
}