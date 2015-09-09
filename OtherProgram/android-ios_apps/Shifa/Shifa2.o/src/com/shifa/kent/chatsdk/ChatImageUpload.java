package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.ActivityNotFoundException;
import android.content.ContentValues;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.media.ExifInterface;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.provider.MediaStore;
import android.provider.MediaStore.MediaColumns;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Toast;

import com.shifa.kent.R;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class ChatImageUpload extends Activity {
    private static final int PICK_FROM_CAMERA = 1;
    private static final int PICK_FROM_GALLERY = 2;
    private static final int PICK_IMAGE = 1;
    private static final int PICK_Camera_IMAGE = 2;
    private static final int SELECT_PICTURE = 1;
    int column_index;
    String filemanagerstring;
    String selectedImagePath;
    Uri imageUri;
    String FBProfilePicURL;
    // Declare our Views, so we can access them later
    String logo, imagePath, Logo;
    MediaPlayer mp = new MediaPlayer();
    String LocationLatLng = ":0$0:";
    Super_Library_AppClass SLAc;
    String title = "";
    String id_web = "";
    String uniqueID = "";
    Context ctx;
    LocationManager myLocationManager;
    String PROVIDER = LocationManager.GPS_PROVIDER;
    private ImageView imgView;
    private ImageButton cancel, gallery, camera;
    private Button btnChatImageSend;
    private Button btnCancelFrame;
    private Bitmap bitmap;
    private String SessionID;
    private ProgressDialog dialog;
    private LocationListener myLocationListener = new LocationListener() {

        @Override
        public void onLocationChanged(Location location) {
            showMyLocation(location);
        }

        @Override
        public void onProviderDisabled(String provider) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onProviderEnabled(String provider) {
            // TODO Auto-generated method stub

        }

        @Override
        public void onStatusChanged(String provider, int status, Bundle extras) {
            // TODO Auto-generated method stub

        }

    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chatimageupload);
        SessionID = RestoreSessionIndexID(".XXXXXXX");
        //FBProfilePicURL = RestoreSessionIndexID("profilepic");
        imgView = (ImageView) findViewById(R.id.ImgViewProfilePic);
        btnChatImageSend = (Button) findViewById(R.id.btnChatImageSend);
        btnCancelFrame = (Button) findViewById(R.id.btnCancelImage);
        gallery = (ImageButton) findViewById(R.id.imgGallerydbtn);
        camera = (ImageButton) findViewById(R.id.imgCameradbtn);
        //cancel = (Button) findViewById(R.id.imgcancelbtn);
        this.SLAc = new Super_Library_AppClass(this);
        ctx = this;
        Bundle extras = getIntent().getExtras();
        getWindow().setSoftInputMode(
                WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN
        );
        if (extras == null) {
        } else {


            title = extras.getString("title"); // important line to save in the database
        }
        uniqueID = UUID.randomUUID().toString();


        btnChatImageSend.setOnClickListener(new OnClickListener() {

            public void onClick(View v) {
                if (bitmap == null) {
                    Toast.makeText(getApplicationContext(),
                            "Please select image", Toast.LENGTH_SHORT).show();
                } else {
                    dialog = ProgressDialog.show(ChatImageUpload.this, "Uploading",
                            "Please wait...", true);

                    uniqueID = UUID.randomUUID().toString();

                    List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
                    nameValuePairs.add(new BasicNameValuePair("selected", uniqueID));

                    nameValuePairs.add(new BasicNameValuePair("id_web", id_web));
                    //    Super_Library_URLV2 SLU2 = new Super_Library_URLV2("http://kent..XXXXXXX/app_php/Shifa4o/WebService/image.php",nameValuePairs, ((Activity) ctx), "ImageUploadComplete");

                    EditText edtText = (EditText) findViewById(R.id.edtTextChat);
                    String ChatTextSend = edtText.getText().toString();
                    String CONST_PUBLIC_CHAT = "PublicChat";

                    String CONST_DISCUSSION_CHAT = "CONST_DISCUSSION_CHAT";
                    String CONST_PRIVATE_MESGGING_CHAT = "PrivateMessageChatting";
                    String PublicChatIdCode = SLAc.GetPreferenceValue("PublicChatIdCode");
                    String id_app = UUID.randomUUID().toString();

                    ShifaDepartment ShifaDepart = new ShifaDepartment(ctx);
                    String ScreenMode = SLAc.GetPreferenceValue("ScreenMode");
                    Log.e("ScreenMode", ScreenMode);
                    if (ScreenMode.equals(CONST_PUBLIC_CHAT)) {
                        // ShifaDepart.PostChatSend(ChatTextSend, PublicChatIdCode, "chat_img/thumb/" + uniqueID, "", id_app, "", "");
                    } else if (ScreenMode.equals(CONST_PRIVATE_MESGGING_CHAT)) {
                        //  ShifaDepart.PostChatSend(ChatTextSend, PublicChatIdCode, "chat_img/thumb/" + uniqueID, "", id_app, SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"));

                    } else if (ScreenMode.equals(CONST_DISCUSSION_CHAT)) {
                        //    ShifaDepart.PostChatSend(ChatTextSend, SLAc.GetPreferenceValue("DiscId"),"chat_img/thumb/" + uniqueID, "", id_app, SLAc.GetPreferenceValue("PvtMsgFriendSessionId"), SLAc.GetPreferenceValue("PvtMsgFriendSessionName"));

                    }
                    Log.e("image", uniqueID);
                    new ChatImageUploadTask().execute();
                    SLAc.SavePreference("ImageChatSend", ChatTextSend);
                    SLAc.SavePreference("Imageid_app", id_app);

                    SLAc.SavePreference("ImagePath", "chat_img/thumb/" + uniqueID);

                    SLAc.SavePreference("ImageUploadSuccess", "true");

                }
            }
        });

        btnCancelFrame.setOnClickListener(new OnClickListener() {
            public void onClick(View v) {
                finish();
            }
        });

        gallery.setOnClickListener(new

                                           OnClickListener() {
                                               public void onClick(View v) {

                                                   Intent intent = new Intent();
                                                   intent.setType("image/*");
                                                   intent.setAction(Intent.ACTION_GET_CONTENT);
                                                   startActivityForResult(Intent.createChooser(intent,
                                                           "Select Picture"), SELECT_PICTURE);


                                               }
                                           }

        );

        camera.setOnClickListener(new

                                          OnClickListener() {
                                              public void onClick(View v) {
/*				// TODO Auto-generated method stub
                //define the file-name to save photo taken by Camera activity
	        	String fileName = "new-photo-name.jpg";
	        	//create parameters for Intent with filename
	        	ContentValues values = new ContentValues();
	        	values.put(MediaStore.Images.Media.TITLE, fileName);
	        	values.put(MediaStore.Images.Media.DESCRIPTION,"Image captured by camera");
	        	//imageUri is the current activity attribute, define and save it for later usage (also in onSaveInstanceState)
	        	imageUri = getContentResolver().insert(MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
	        	//create new Intent
	        	Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
	        	intent.putExtra(MediaStore.EXTRA_OUTPUT, imageUri);
	        	intent.putExtra(MediaStore.EXTRA_VIDEO_QUALITY, 1);
	        	startActivityForResult(intent, PICK_Camera_IMAGE);
	*/

                                                  try {


                                                      Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                                                      intent.putExtra(MediaStore.EXTRA_OUTPUT,
                                                              MediaStore.Images.Media.EXTERNAL_CONTENT_URI.toString());
                                                      startActivityForResult(Intent.createChooser(intent,
                                                              "Camera"), SELECT_PICTURE);

                                                  } catch (ActivityNotFoundException e) {
                                                      // Do nothing for now

                                                  }

                                              }
                                          }

        );


        Intent intent = new Intent();
        intent.setType("image/*");
        intent.setAction(Intent.ACTION_GET_CONTENT);
        startActivityForResult(Intent.createChooser(intent,
                "Select Picture"), SELECT_PICTURE);

            /*if (uniqueID != ""){
                new DownloadImageTask((ImageView) imgView).execute("http://kent..XXXXXXX/app_php/shifaappsettings/user_imageupload/"+uniqueID+".jpg");
                //new DownloadImageTask((ImageView) imgView).execute(FBProfilePicURL);
            }*/
        //     ShowAlert();
        //  Gallery_OPEN();
    }

    private void ShowAlert() {
        AlertDialog.Builder builderSingle = new AlertDialog.Builder(
                ChatImageUpload.this);
        builderSingle.setIcon(R.drawable.ic_editor_attach_file);
        builderSingle.setTitle("Select Media:-");
        final ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(
                ChatImageUpload.this,
                android.R.layout.select_dialog_singlechoice);
        arrayAdapter.add("Gallery");
        arrayAdapter.add("Camera");
        builderSingle.setNegativeButton("Cancel",
                new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();

                    }
                });

        builderSingle.setAdapter(arrayAdapter,
                new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int which) {

                        if (which == 0) {
                            Gallery_OPEN();
                        } else if (which == 1) {
                            Camera_OPEN();
                        }

                    }
                });
        builderSingle.show();
    }

    private void showMyLocation(Location l) {
        if (l == null) {
            LocationLatLng = ":0$0:";
        } else {
            LocationLatLng = ":" + l.getLatitude() + "$" + l.getLongitude() + ":";

        }

    }

    private void doCrop(String filePath) {
        Intent intent = new Intent("com.android.camera.action.CROP");
        intent.setClassName("com.android.camera", "com.android.camera.CropImage");
        File file = new File(filePath);
        Uri uri = Uri.fromFile(file);
        intent.setData(uri);
        intent.putExtra("crop", "true");
        intent.putExtra("aspectX", 1);
        intent.putExtra("aspectY", 1);
        intent.putExtra("outputX", 96);
        intent.putExtra("outputY", 96);
        intent.putExtra("noFaceDetection", true);
        intent.putExtra("return-data", true);
        startActivityForResult(intent, 1);


    }

    private String RestoreSessionIndexID(String SessionKey) {

        SharedPreferences prefs = getSharedPreferences("AppNameSettings", 0);
        String restoredText = prefs.getString(SessionKey, null);
        if (restoredText != null) {
            return restoredText;
        }
        return "";

    }

    private void Gallery_OPEN() {
        try {
            Intent gintent = new Intent();
            gintent.setType("image/*");
            gintent.setAction(Intent.ACTION_GET_CONTENT);
            startActivityForResult(
                    Intent.createChooser(gintent, "Select Picture"),
                    PICK_IMAGE);
        } catch (Exception e) {
            Toast.makeText(getApplicationContext(),
                    e.getMessage(),
                    Toast.LENGTH_LONG).show();
            Log.e(e.getClass().getName(), e.getMessage(), e);
        }
    }

    private void Camera_OPEN() {
        try {
            //define the file-name to save photo taken by Camera activity
            String fileName = "new-photo-name.jpg";
            //create parameters for Intent with filename
            ContentValues values = new ContentValues();
            values.put(MediaStore.Images.Media.TITLE, fileName);
            values.put(MediaStore.Images.Media.DESCRIPTION, "Image captured by camera");
            //imageUri is the current activity attribute, define and save it for later usage (also in onSaveInstanceState)
            imageUri = getContentResolver().insert(MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
            //create new Intent
            Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
            intent.putExtra(MediaStore.EXTRA_OUTPUT, imageUri);
            intent.putExtra(MediaStore.EXTRA_VIDEO_QUALITY, 1);
            startActivityForResult(intent, PICK_Camera_IMAGE);
        } catch (Exception ex) {

        }
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {

        try {
            String filePath = null;
            Log.e("ImageUpload", "1 " + requestCode);
            if (requestCode == SELECT_PICTURE) {
                Uri selectedImageUri = data.getData();
                //OI FILE Manager
                filemanagerstring = selectedImageUri.getPath();

                //MEDIA GALLERY
                selectedImagePath = getPath(selectedImageUri);
                imgView.setImageURI(selectedImageUri);
                //  imagePath.getBytes();
                Log.e("imgpath", imagePath.toString());

                //imagePath.getBytes();
                //      bitmap = BitmapFactory.decodeFile(imagePath);
                //  Log.e("ImageUpload","decodeFile " + selectedImageUri.getPath().toString());
                bitmap = BitmapFactory.decodeFile(imagePath);

                //Log.e("ImageUpload","selectedImageUri " + selectedImageUri.getPath().toString());
                //   decodeFile(imagePath);

/*
                Log.e("ImageUpload","2");
				Bundle extras = data.getExtras();
				Log.e("ImageUpload","4 "+extras);
				if (extras != null) {
					Log.e("ImageUpload","3");
					Bitmap photo = extras.getParcelable("data");
					bitmap = photo;
					imgView.setImageBitmap(photo);
					decodeFile("/sdcard/shifa/profilepic.jpg");
					
				}
                else{
                    Log.e("ImageUpload","Image not capture");
                }
                */
            }


            if (requestCode == PICK_FROM_GALLERY) {
                Bundle extras2 = data.getExtras();
                if (extras2 != null) {
                    Bitmap photo = extras2.getParcelable("data");
                    bitmap = photo;
                    imgView.setImageBitmap(photo);

                    decodeFile("/sdcard/shifa/profilepic.jpg");


                }
            }
        } catch (Exception ex) {
            Log.e("ImageUpload", "error " + ex.toString());
        }
    }

/*
				
				
				
				
				
				
		switch (requestCode) {
				case PICK_IMAGE:
					if (resultCode == Activity.RESULT_OK) {
						//selectedImageUri = data.getData();
						Bundle extras2 = data.getExtras();
		 		    	Bitmap photo = extras2.getParcelable("data");
						 imgView.setImageBitmap(photo);
					}
					break;
				case PICK_Camera_IMAGE:
					 if (resultCode == RESULT_OK) {
						 

								 
		 		        //use imageUri here to access the image
		 		    	selectedImageUri = imageUri;
		 		    	/*Bitmap mPic = (Bitmap) data.getExtras().get("data");
						//selectedImageUri = Uri.parse(MediaStore.Images.Media.insertImage(getContentResolver(), mPic, getResources().getString(R.string.app_name), Long.toString(System.currentTimeMillis())));
				    } else if (resultCode == RESULT_CANCELED) {
		 		        Toast.makeText(this, "Picture was not taken", Toast.LENGTH_SHORT).show();
		 		    } else {
		 		    	Toast.makeText(this, "Picture was not taken", Toast.LENGTH_SHORT).show();
		 		    }
					 break;
			}
		
			if(selectedImageUri != null){
					try {
						// OI FILE Manager
						String filemanagerstring = selectedImageUri.getPath();
			
						// MEDIA GALLERY
						String selectedImagePath = getPath(selectedImageUri);
			
						if (selectedImagePath != null) {
							filePath = selectedImagePath;
						} else if (filemanagerstring != null) {
							filePath = filemanagerstring;
						} else {
							Toast.makeText(getApplicationContext(), "Unknown path",
									Toast.LENGTH_LONG).show();
							Log.e("Bitmap", "Unknown path");
						}
			
						if (filePath != null) {
							decodeFile(filePath);
							doCrop(filePath);
						} else {
							bitmap = null;
						}
					} catch (Exception e) {
						Toast.makeText(getApplicationContext(), "Internal error",
								Toast.LENGTH_LONG).show();
						Log.e(e.getClass().getName(), e.getMessage(), e);
					}
			}
	*/
    //}

    //UPDATED!
    public String getPath(Uri uri) {
        String[] projection = {MediaColumns.DATA};
        Cursor cursor = managedQuery(uri, projection, null, null, null);
        column_index = cursor
                .getColumnIndexOrThrow(MediaColumns.DATA);
        cursor.moveToFirst();
        imagePath = cursor.getString(column_index);

        return cursor.getString(column_index);
    }

    public void decodeFile(String filePath) {

        //bitmap = BitmapFactory.decodeFile(imagePath);
        //Log.e("Bitmap","Bitmpa loaded  " + bitmap);
        bitmap = decodeFile1(filePath);
        //imgView.setImageBitmap(bitmap);

    }

    public Bitmap decodeFile1(String path) {//you can provide file path here
        int orientation;
        try {
            if (path == null) {
                return null;
            }
            // decode image size
            BitmapFactory.Options o = new BitmapFactory.Options();
            o.inJustDecodeBounds = true;
            // Find the correct scale value. It should be the power of 2.
            final int REQUIRED_SIZE = 70;
            int width_tmp = o.outWidth, height_tmp = o.outHeight;
            int scale = 0;
            while (true) {
                if (width_tmp / 2 < REQUIRED_SIZE
                        || height_tmp / 2 < REQUIRED_SIZE)
                    break;
                width_tmp /= 2;
                height_tmp /= 2;
                scale++;
            }
            // decode with inSampleSize
            BitmapFactory.Options o2 = new BitmapFactory.Options();
            o2.inSampleSize = scale;
            //Bitmap bm = BitmapFactory.decodeFile(path, o2);
            Bitmap bm = bitmap;
            Bitmap bitmap = bm;

            ExifInterface exif = new ExifInterface(path);

            orientation = exif
                    .getAttributeInt(ExifInterface.TAG_ORIENTATION, 1);

            Log.e("ExifInteface .........", "rotation =" + orientation);

//          exif.setAttribute(ExifInterface.ORIENTATION_ROTATE_90, 90);

            Log.e("orientation", "" + orientation);
            Matrix m = new Matrix();

            if ((orientation == ExifInterface.ORIENTATION_ROTATE_180)) {
                m.postRotate(180);
//              m.postScale((float) bm.getWidth(), (float) bm.getHeight());
                // if(m.preRotate(90)){
                Log.e("in orientation", "" + orientation);
                bitmap = Bitmap.createBitmap(bm, 0, 0, 50,
                        50, m, true);
                return bitmap;
            } else if (orientation == ExifInterface.ORIENTATION_ROTATE_90) {
                m.postRotate(90);
                Log.e("in orientation", "" + orientation);
                bitmap = Bitmap.createBitmap(bm, 0, 0, 50,
                        50, m, true);
                return bitmap;
            } else if (orientation == ExifInterface.ORIENTATION_ROTATE_270) {
                m.postRotate(270);
                Log.e("in orientation", "" + orientation);
                bitmap = Bitmap.createBitmap(bm, 0, 0, 50,
                        50, m, true);
                return bitmap;
            }
            return bitmap;
        } catch (Exception e) {
            Log.e("error in decodeing ", e.toString());
            return null;
        }

    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {

            //Intent intent = new Intent(ChatImageUpload.this, home_menu.class);
            // startActivity(intent);

            finish();

            return true;
        }
        return super.onKeyDown(keyCode, event);
    }

    private class DownloadImageTask extends AsyncTask<String, Void, Bitmap> {
        ImageView bmImage;

        public DownloadImageTask(ImageView bmImage) {
            this.bmImage = bmImage;
        }

        protected Bitmap doInBackground(String... urls) {
            String urldisplay = urls[0];
            Bitmap mIcon11 = null;
            try {
                InputStream in = new java.net.URL(urldisplay).openStream();
                mIcon11 = BitmapFactory.decodeStream(in);
            } catch (Exception e) {
                Log.e("Error", e.getMessage());
                e.printStackTrace();
            }
            return mIcon11;
        }

        protected void onPostExecute(Bitmap result) {
            if (result == null) return;
            bmImage.setImageBitmap(result);
        }
    }

    class ChatImageUploadTask extends AsyncTask<Void, Void, String> {
        @SuppressWarnings("unused")
        @Override
        protected String doInBackground(Void... unsued) {
            InputStream is;
            BitmapFactory.Options bfo;
            Bitmap bitmapOrg;
            ByteArrayOutputStream bao;

            bfo = new BitmapFactory.Options();
            bfo.inSampleSize = 2;
            //bitmapOrg = BitmapFactory.decodeFile(Environment.getExternalStorageDirectory() + "/" + customImage, bfo);

            bao = new ByteArrayOutputStream();
            bitmap.compress(Bitmap.CompressFormat.JPEG, 90, bao);
            byte[] ba = bao.toByteArray();
            String ba1 = Base64.encodeBytes(ba);
            ArrayList<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
            nameValuePairs.add(new BasicNameValuePair("image", ba1));
            nameValuePairs.add(new BasicNameValuePair("cmd", "image_android"));
            Log.v("log_tag", System.currentTimeMillis() + ".jpg");
            try {
                HttpClient httpclient = new DefaultHttpClient();
                HttpPost httppost = new HttpPost("http://kent..XXXXXXX/app_php/shifaappsettings/chat_img/uploadimage.php?.XXXXXXX=" + uniqueID);
                httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                HttpResponse response = httpclient.execute(httppost);
                String inputLine;
                BufferedReader in = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));
                try {
                    while ((inputLine = in.readLine()) != null) {
                        Log.e(" response", inputLine);
                    }
                    in.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
                //  Log.e("URL response", is.toString());
                Log.e("log_tag", "In the try Loop");
                //  return response;
            } catch (Exception e) {
                Log.e("log_tag", "Error in http connection " + e.toString());
            }
            return "Success";
            // (null);
        }

        @Override
        protected void onProgressUpdate(Void... unsued) {

        }

        @Override
        protected void onPostExecute(String sResponse) {
            try {
                Log.e("Response", sResponse);
                //SLAc.PostWebApi(new String[] {title + "~" + SessionID + "-:-" + uniqueID, "http://shifa.in/api/RepertoryService/ExtraSaveImgPath"});
                if (dialog.isShowing())
                    dialog.dismiss();


                finish();


            } catch (Exception e) {

            }
        }

    }


}

