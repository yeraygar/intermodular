package inter.intermodular.support

import com.orhanobut.logger.Logger
import java.nio.charset.StandardCharsets
import java.security.MessageDigest
/** Recibe un string y lo devuelve cifrado */
fun getSHA256(passw : String) : String {
    try{
        val bytes = passw.toString().toByteArray()
        val md = MessageDigest.getInstance("SHA-256")
        val digest = md.digest(bytes)
        return digest.fold("") { str, it -> str + "%02x".format(it) }

    }catch (e: Exception){
        Logger.wtf("ERROR de conversion SHA-256 ${e.toString()}")
        throw e
    }
}
