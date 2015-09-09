package com.bravocodesolutions.nfctest;

import android.app.PendingIntent;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.nfc.tech.Ndef;
import android.nfc.tech.NdefFormatable;
import android.nfc.tech.NfcA;
import android.os.Parcelable;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.io.ByteArrayOutputStream;
import java.io.UnsupportedEncodingException;
import java.util.Locale;


public class MainActivity extends ActionBarActivity {
    NfcAdapter nfcAdapter;
    Button saveButton;
    EditText editText;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editText = (EditText) findViewById(R.id.nfcEditText);
        saveButton = (Button) findViewById(R.id.saveButton);
        nfcAdapter = NfcAdapter.getDefaultAdapter(this);
        if(nfcAdapter != null && nfcAdapter.isEnabled()){
            Toast.makeText(MainActivity.this, "NFC Available", Toast.LENGTH_SHORT).show();
        }else{
            Toast.makeText(MainActivity.this,"NFC Not available", Toast.LENGTH_SHORT).show();
        }

    }

    @Override
    protected void onResume() {
        super.onResume();

        enableForegroundDispatchSystem();
    }

    @Override
    protected void onNewIntent(final Intent intent) {
        super.onNewIntent(intent);

        if(intent.hasExtra(NfcAdapter.EXTRA_TAG)){
            Toast.makeText(this, "NFC Intent!", Toast.LENGTH_SHORT).show();

            saveButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);
                    NdefMessage ndefMessage = createNdefMessage(editText.getText().toString());
                    writeNdefMessage(tag, ndefMessage);

                }
            });


        }

    }

    @Override
    protected void onPause() {
        super.onPause();
        disableForegroundDispatchSystem();
    }

    private void disableForegroundDispatchSystem(){
        nfcAdapter.disableForegroundDispatch(this);
    }
    private void enableForegroundDispatchSystem(){
        Intent intent = new Intent(this, MainActivity.class).addFlags(Intent.FLAG_RECEIVER_REPLACE_PENDING);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0, intent, 0);
        IntentFilter[] intentFilters = new IntentFilter[]{};
        nfcAdapter.enableForegroundDispatch(this, pendingIntent, intentFilters, null);
    }

    @Override
    protected void onPostResume() {
        super.onResume();
    }

    private void formatTag(Tag tag, NdefMessage message){
        try{
            NdefFormatable ndefFormatable = NdefFormatable.get(tag);
            if(ndefFormatable == null){
                Toast.makeText(this, "This tag is not formattable", Toast.LENGTH_SHORT).show();
            }

            ndefFormatable.connect();
            ndefFormatable.format(message);
            ndefFormatable.close();

            Toast.makeText(this,"Tag written!",Toast.LENGTH_SHORT).show();


        }catch (Exception e){
            Log.e("format Tag", e.getMessage());
        }
    }

    private void writeNdefMessage(Tag tag, NdefMessage ndefMessage){
        try{
            if ((tag == null)){
                Toast.makeText(this, "Tag object cannot be NULL", Toast.LENGTH_SHORT).show();
                return;
            }

            Ndef ndef = Ndef.get(tag);

            if(ndef == null){
                //format tag with the ndef format and writes the message
                formatTag(tag, ndefMessage);
            }else{
                ndef.connect();
                if(!ndef.isWritable()){
                    Toast.makeText(this,"Tag is not writable",Toast.LENGTH_SHORT).show();
                    ndef.close();
                    return;
                }
                ndef.writeNdefMessage(ndefMessage);
                ndef.close();

                Toast.makeText(this,"Tag written!",Toast.LENGTH_SHORT).show();

            }

        }catch (Exception e){
            Log.w("writeNdefMessage", e.getMessage());
        }
    }

    private NdefRecord createTextRecord(String content){
        try{
            byte[] language;
            language = Locale.getDefault().getLanguage().getBytes("UTF-8");

            final byte[] text = content.getBytes("UTF-8");
            final int languageSize = language.length;
            final int textLength = text.length;
            final ByteArrayOutputStream payLoad = new ByteArrayOutputStream(1 + languageSize + textLength);
            payLoad.write((byte)(languageSize & 0x1F));
            payLoad.write(language, 0, languageSize);
            payLoad.write(text, 0, textLength);

            return new NdefRecord(NdefRecord.TNF_WELL_KNOWN, NdefRecord.RTD_TEXT,new byte[0],payLoad.toByteArray());

        }catch (UnsupportedEncodingException e){
            Log.e("createTextRecord", e.getMessage());
        }
        return  null;
    }

    private NdefMessage createNdefMessage(String messageInput){
        NdefRecord ndefRecord = createTextRecord(messageInput);
        NdefMessage ndefMessage = new NdefMessage(new NdefRecord[]{ ndefRecord });
        return  ndefMessage;
    }

}
