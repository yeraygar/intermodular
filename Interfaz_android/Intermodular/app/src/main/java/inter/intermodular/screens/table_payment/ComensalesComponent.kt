package inter.intermodular.screens.table_payment

import android.content.Context
import android.widget.Toast
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Groups
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
import inter.intermodular.R
import inter.intermodular.support.currentTable
import inter.intermodular.view_models.TableViewModel
import java.lang.Exception

@Composable
fun ShowAlertDialogComensales(
    isComensalesOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    applicationContext: Context
) {
    var comensales  = remember { mutableStateOf(0) }
    var aceptarEnabled = remember { mutableStateOf(false) }
    if (comensales.value <= currentTable.comensalesMax && comensales.value > 0){
        currentTable.comensales = comensales.value
        aceptarEnabled.value = true
    }
    else {
        Toast.makeText(applicationContext, "Comensales entre [1 - ${currentTable.comensalesMax}]", Toast.LENGTH_SHORT).show()
    }

    Dialog(onDismissRequest = { isComensalesOpen.value = false }) {
        Surface(
            modifier = Modifier
                //.fillMaxSize()
                .padding(10.dp, 20.dp),
            shape = RoundedCornerShape(5.dp),
            color = Color.White
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center
            ) {

                OutlinedTextField(
                    value = comensales.value.toString(),
                    onValueChange = { it.also {
                        try{
                            comensales.value = it.toInt()

                        }catch (e: Exception){
                            Toast.makeText(applicationContext, "Caracteres Invalidos", Toast.LENGTH_SHORT).show()
                        }
                    } },
                    label = { Text(text = "Numero de Comensales", style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)
                    ),) },
                    placeholder = { Text(text = "Numero de Comensales",style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)
                    ),) },
                    singleLine = true,
                    keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                    trailingIcon = {
                        val image = Icons.Filled.Groups
                        IconButton(onClick = {  }
                        ) { Icon(imageVector  = image, "Comensales") }
                    },
                    colors = TextFieldDefaults.outlinedTextFieldColors(
                        focusedBorderColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedBorderColor = colorResource(id = R.color.gris_claro),
                        focusedLabelColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedLabelColor = colorResource(id = R.color.gris_claro),
                        cursorColor = colorResource(id = R.color.azul)
                    ),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp, 10.dp, 10.dp, 5.dp),
                )

                Spacer(modifier = Modifier.height(5.dp))


                Button(
                    enabled = aceptarEnabled.value,
                    onClick = {
                        if (comensales.value <= currentTable.comensalesMax && comensales.value > 0) {
                            isComensalesOpen.value = false
                            aceptarEnabled.value = false
                            tableViewModel.updateTable(currentTable, currentTable._id)
                        }else{
                            Toast.makeText(applicationContext, "Valor Incorrecto [1 - ${currentTable.comensalesMax}] ", Toast.LENGTH_SHORT).show()
                        }
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(60.dp)
                        .padding(10.dp),
                    shape = RoundedCornerShape(5.dp),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),
                ) {
                    Text(
                        text = "ACEPTAR",
                        color = Color.White,
                        fontSize = 12.sp
                    )
                }

                Spacer(modifier = Modifier.height(5.dp))

                Button(
                    //  enabled = isAceptarEnabled.value,
                    onClick = {
                        isComensalesOpen.value = false
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(60.dp)
                        .padding(10.dp),
                    shape = RoundedCornerShape(5.dp),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.rojo)),
                ) {
                    Text(
                        text = "CANCELAR",
                        color = Color.White,
                        fontSize = 12.sp
                    )
                }
            }
        }
    }
}