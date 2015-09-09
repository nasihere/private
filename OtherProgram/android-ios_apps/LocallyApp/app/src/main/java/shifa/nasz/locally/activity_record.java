package shifa.nasz.locally;



import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.widget.Button;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class activity_record extends Activity {

    final static int RQS_RECORDING = 1;

    Uri savedUri;

    private static final int SELECT_AUDIO = 2;
    public String SessionID  = "";
    String selectedPath = "";
    String AudioUniqueID = "";
    Button buttonRecord;
    Super_Library_AppClass SLAc;
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
       // setContentView(R.layout.recording);
       // buttonRecord = (Button)findViewById(R.id.record);
        SLAc = new Super_Library_AppClass(this);
        SessionID  = SLAc.RestoreSessionIndexID("session_id");
        Bundle extras = getIntent().getExtras();
        if (extras.getString("AudioUniqueID") != null){
            AudioUniqueID = extras.getString("AudioUniqueID").toString();
        }

        SLAc.SavePreference("Audio","");
        // TODO Auto-generated method stub
        Intent intent =
                new Intent(MediaStore.Audio.Media.RECORD_SOUND_ACTION);
        startActivityForResult(intent, RQS_RECORDING);
        /*
        buttonRecord.setOnClickListener(new Button.OnClickListener(){

            @Override
            public void onClick(View arg0) {

            }});
            */
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        // TODO Auto-generated method stub
        if(requestCode == RQS_RECORDING){

            if (data == null) {
                finish();
                return;
            }
            savedUri = data.getData();

            Uri selectedImageUri = data.getData();
            selectedPath = getPath(selectedImageUri);
            System.out.println("SELECT_AUDIO Path : " + selectedPath);

            Thread thread = new Thread(new Runnable(){
                @Override
                public void run() {
                    try {
                        //doFileUpload();
                        uploadFile(selectedPath);
                        SLAc.SavePreference("Audio","Recording Saved");
                        finish();

                        //Your code goes here
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });

            thread.start();




        }
        else{
            finish();

        }

    }
    public String getPath(Uri uri) {
        String[] projection = { MediaStore.Images.Media.DATA };
        Cursor cursor = managedQuery(uri, projection, null, null, null);
        int column_index = cursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }
    public int uploadFile(String sourceFileUri) {
        String upLoadServerUri = "http://kent.nasz.us/mumbra/php/webaudioupload.php?session_id="+SessionID + "&AudioUniqueID="+AudioUniqueID+".3gpp";
        String fileName = sourceFileUri;

        HttpURLConnection conn = null;
        DataOutputStream dos = null;
        String lineEnd = "\r\n";
        String twoHyphens = "--";
        String boundary = "*****";
        int bytesRead, bytesAvailable, bufferSize;
        byte[] buffer;
        int maxBufferSize = 1 * 1024 * 1024;
        File sourceFile = new File(sourceFileUri);
        if (!sourceFile.isFile()) {
            Log.e("uploadFile", "Source File Does not exist");
            return 0;
        }
        try { // open a URL connection to the Servlet
            FileInputStream fileInputStream = new FileInputStream(sourceFile);
            URL url = new URL(upLoadServerUri);
            conn = (HttpURLConnection) url.openConnection(); // Open a HTTP  connection to  the URL
            conn.setDoInput(true); // Allow Inputs
            conn.setDoOutput(true); // Allow Outputs
            conn.setUseCaches(false); // Don't use a Cached Copy
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Connection", "Keep-Alive");
            conn.setRequestProperty("ENCTYPE", "multipart/form-data");
            conn.setRequestProperty("Content-Type", "multipart/form-data;boundary=" + boundary);
            conn.setRequestProperty("uploaded_file", fileName);
            dos = new DataOutputStream(conn.getOutputStream());

            dos.writeBytes(twoHyphens + boundary + lineEnd);
            dos.writeBytes("Content-Disposition: form-data; name=\"uploaded_file\";filename=\""+ fileName + "\"" + lineEnd);
            dos.writeBytes(lineEnd);

            bytesAvailable = fileInputStream.available(); // create a buffer of  maximum size

            bufferSize = Math.min(bytesAvailable, maxBufferSize);
            buffer = new byte[bufferSize];

            // read file and write it into form...
            bytesRead = fileInputStream.read(buffer, 0, bufferSize);

            while (bytesRead > 0) {
                dos.write(buffer, 0, bufferSize);
                bytesAvailable = fileInputStream.available();
                bufferSize = Math.min(bytesAvailable, maxBufferSize);
                bytesRead = fileInputStream.read(buffer, 0, bufferSize);
            }

            // send multipart form data necesssary after file data...
            dos.writeBytes(lineEnd);
            dos.writeBytes(twoHyphens + boundary + twoHyphens + lineEnd);

            // Responses from the server (code and message)
            int serverResponseCode = 0;
            serverResponseCode = conn.getResponseCode();
            String serverResponseMessage = conn.getResponseMessage();

            Log.i("uploadFile", "HTTP Response is : " + serverResponseMessage + ": " + serverResponseCode);
            try {
                DataInputStream inStream  = new DataInputStream ( conn.getInputStream() );
                String str;

                while (( str = inStream.readLine()) != null)
                {
                    Log.e("Debug","Server Response "+str);
                }
                inStream.close();

            }
            catch (IOException ioex){
                Log.e("Debug", "error: " + ioex.getMessage(), ioex);
            }


            //close the streams //
            fileInputStream.close();
            dos.flush();
            dos.close();

        } catch (MalformedURLException ex) {
            ex.printStackTrace();
            Log.e("Upload file to server", "error: " + ex.getMessage(), ex);
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("Upload file to server Exception", "Exception : " + e.getMessage(), e);
        }
        return 0;
    }



}