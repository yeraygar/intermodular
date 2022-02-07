package inter.intermodular.screens.map_tables

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.Button
import androidx.compose.material.ScaffoldState
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.orhanobut.logger.Logger
import inter.intermodular.support.currentZone
import inter.intermodular.support.currentZoneTables
import inter.intermodular.view_models.MapViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch


@Composable
public fun MapZoneComponent(
    mapViewModel: MapViewModel,
    title: MutableState<String>,
    scope: CoroutineScope,
    scaffoldState: ScaffoldState
) {
    LazyColumn(
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.spacedBy(15.dp),
        modifier = Modifier
            .fillMaxWidth()
            .padding(10.dp, 5.dp)
    ) {
        items(mapViewModel.clientZonesResponse.count()) { i ->
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
                modifier = Modifier
                    .fillMaxWidth()
                    .height(50.dp)
            ) {
                Text(text = mapViewModel.clientZonesResponse[i].zone_name)

            }
        }


    }
}