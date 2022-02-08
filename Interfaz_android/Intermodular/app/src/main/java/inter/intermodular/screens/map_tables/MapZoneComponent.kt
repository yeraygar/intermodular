package inter.intermodular.screens.map_tables

import android.content.res.Resources
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavHostController
import com.orhanobut.logger.Logger
import inter.intermodular.support.currentZone
import inter.intermodular.support.currentZoneTables
import inter.intermodular.view_models.MapViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch
import inter.intermodular.R
import org.intellij.lang.annotations.JdkConstants


@Composable
fun MapZoneComponent(
    mapViewModel: MapViewModel,
    title: MutableState<String>,
    scope: CoroutineScope,
    scaffoldState: ScaffoldState
) {

    Card (
        modifier = Modifier
            .fillMaxWidth()
            .height(80.dp),

       // backgroundColor = colorResource(id = R.color.azul_oscuro)
            ) {
       Column(
           horizontalAlignment = Alignment.CenterHorizontally,
           verticalArrangement = Arrangement.Center ) {
           Text(
               text = "ELIGE UNA ZONA",
               fontSize = 20.sp,
               fontWeight = FontWeight.Bold,
               color = colorResource(id = R.color.azul_oscuro)
           )
       }
    }

    LazyColumn(
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.spacedBy(15.dp),
        modifier = Modifier
            .fillMaxWidth()
            .padding(10.dp, 5.dp)
    ) {
        items(mapViewModel.clientZonesResponse.count()) { i ->
            Spacer(modifier = Modifier.height(10.dp))
            Button(
                onClick = {
                    currentZone = mapViewModel.clientZonesResponse[i]
                    Logger.i("CurrentZone: ${currentZone!!.zone_name}")
                    mapViewModel.getZoneTables(mapViewModel.clientZonesResponse[i]._id)
                    currentZoneTables = mapViewModel.zoneTablesResponse
                    title.value = currentZone!!.zone_name
                    mapViewModel.getZoneTables(currentZone!!._id)
                    if (!mapViewModel.zoneTablesResponse.isNullOrEmpty()) {
                        currentZoneTables = mapViewModel.zoneTablesResponse
                        scope.launch {
                            scaffoldState.drawerState.close()
                        }
                    }
                },

                colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
                modifier = Modifier
                    .fillMaxWidth()
                    .height(50.dp),
            ) {
                Text(
                    text = mapViewModel.clientZonesResponse[i].zone_name,
                    color = Color.White,
                    fontWeight = FontWeight.Bold
                )
            }
        }
    }
}