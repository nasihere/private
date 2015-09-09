package shifa.nasz.locally;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.media.AudioManager;
import android.media.MediaPlayer;
import android.net.Uri;
import android.text.Html;
import android.util.Log;
import android.view.Display;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.HorizontalScrollView;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.RatingBar;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;

public class UserCustomAdapter extends ArrayAdapter<User> implements Filterable  {
	Context context;
	int layoutResourceId;
	DBclass db1;
	private ProgressBar pb;
	private boolean gotdata;
	UserHolder holder = null;
	String ShifaInteractiveFlag;
	public ImageManager imageManager;
	boolean filterResult = true;
    boolean isPLAYING = false;
    Super_Library_AppClass SLAc;
	ProgressDialog progressDialog ;
    MediaPlayer player;
    String MyAccountName;
    final ArrayList<User> data;//new ArrayList<User>();
	ArrayList<User> filterdata = new ArrayList<User>();
	String SmartCategoy = "";
	int iCounterForComment = 0;

	public UserCustomAdapter(Context context, int layoutResourceId,
			 ArrayList<User> arrdata, String Category) {
		super(context, layoutResourceId, arrdata);
		this.layoutResourceId = layoutResourceId;
		this.context = context;
		this.filterdata = arrdata;
		this.data = arrdata;
		this.SmartCategoy = Category;
		db1 = new DBclass(context);
        SLAc = new Super_Library_AppClass(context);
        MyAccountName = SLAc.GetPreferenceValue("ProfileName") + " - " + SLAc.GetPreferenceValue("ProfileMobile");
		imageManager = 
				new ImageManager(context.getApplicationContext());
		Log.e("USercustomadapter constructor","Data Populate done");



		

	}
	@Override
	public int getCount() {
	    return filterdata.size();
	}
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		Log.e("runnable","convertView");
		
		View row = convertView;
		Log.e("row","Start");

		if (row == null) {
			Log.e("cache","false");
			LayoutInflater inflater = ((Activity) context).getLayoutInflater();
			Log.e("cache","3.0.8");
			row = inflater.inflate(layoutResourceId, parent, false);
			Log.e("cache","3.0.9");
			holder = new UserHolder();
			Log.e("cache","3.1.0");
			holder.textName = (TextView) row.findViewById(R.id.txtName_l_r_d);
			Log.e("cache","3.2");
			holder.txtCounter = (TextView) row.findViewById(R.id.txtCounter_l_r_d);
            holder.objTimeHour = (TextView) row.findViewById(R.id.objmenuhour);
            holder.objPrice = (TextView) row.findViewById(R.id.objPrice);

			Log.e("cache","3.3");
			holder.txtImageCount_l_r_d  = (TextView) row.findViewById(R.id.txtImageCount_l_r_d);
			Log.e("cache","3.4");
			Log.e("cache","3.5");
			holder.btnDelete = (ImageView) row.findViewById(R.id.imgRem_l_r_d);
			Log.e("cache","3.6");
			holder.txtLike = (TextView) row.findViewById(R.id.txtLike_l_r_d);
			Log.e("cache","3.8");
			holder.txtComment = (TextView) row.findViewById(R.id.txtComment_l_r_d);
			holder.imgReportIncorrect =  (ImageView) row.findViewById(R.id.imgBtn_Report_l_r_d);

			Log.e("cache","3.10");
			holder.imgPost = (ImageView) row.findViewById(R.id.imgView_Repertory_l_r_d);
            holder.xml_full_img_hor_below_view = (LinearLayout) row.findViewById(R.id.xml_full_img_hor_below_view);
			Log.e("cache","3.11");
			holder.ratBar = (RatingBar) row.findViewById(R.id.ratBar);
			Log.e("cache","3.12");
            holder.imgMedia = (ImageButton) row.findViewById(R.id.imgMedia);
		   holder.relSmartPicture = (HorizontalScrollView) row.findViewById(R.id.relSmartPicture);
            holder.imgCallButton = (ImageButton) row.findViewById(R.id.imgCallButton);
            holder.imgGallery = (ImageButton) row.findViewById(R.id.imgGallery);
            holder.imgWebView = (ImageButton) row.findViewById(R.id.imgWebView);

            holder.txtReviewInfo = (TextView) row.findViewById(R.id.txtReviewInfo);
            holder.webSummary = (TextView) row.findViewById(R.id.webSummary);
            holder.WebSummaryHTML = (WebView) row.findViewById(R.id.WebSummaryHTML);

            holder.imgInternet = (ImageButton) row.findViewById(R.id.imgInternet);
            holder.imgArrow = (ImageView) row.findViewById(R.id.imgkentArrow);
			Log.e("cache","3.13");
			holder.relSmartTabKent = (RelativeLayout) row.findViewById(R.id.relSmartTabKent);
			Log.e("cache","3.14");
			holder.relSmartComment = (LinearLayout) row.findViewById(R.id.relSmartComment);

            holder.relSmartCommentItem = (RelativeLayout) row.findViewById(R.id.relSmartCommentItem);

			Log.e("cache","3.15");

			Log.e("cache", "3.1");

         	Log.e("cache","4");
			holder.btnDelete.setTag(R.string.RemediesShowHide, holder);
            holder.imgGallery.setTag(R.string.RemediesShowHide, holder);
            holder.txtReviewInfo.setTag(R.string.RemediesShowHide, holder);
            holder.imgWebView.setTag(R.string.RemediesShowHide, holder);
            holder.ratBar.setTag(R.string.RemediesShowHide, holder);

            row.setTag(holder);
			Log.e("cache","5");
		} else {
			Log.e("cache","true");

			holder = (UserHolder) row.getTag();
			Log.e("cache","3.17");
		}
                Log.e("Getview",String.valueOf(position));
				try
				{
				User user = filterdata.get(position);

                    if (user.selected.indexOf("_logo") != -1){
                        // suppose in selected images lists if id_web number exists so load asncy image and set as logo
                        final String[] ImageArrayList = user.selected.split(",");

                        for(int i = 0; i <= ImageArrayList.length -1 ;i++) {
                            String ImageURL = ImageArrayList[i];
                            if (ImageURL.indexOf("_logo") != -1){
                                imageManager.displayImage(ImageURL+"_thumb", holder.btnDelete, R.drawable.ic_screen_shot_2015_01_21_at_5);

                            }
                        }

                    }
                    if (user.getCategory().indexOf(", ") != -1) { // checking r u in category page or in deep link.. example... suppose u r in menu of Sahil Restaruarnt so restaruant icon it will set
                        String[] CateogoryLast = user.getCategory().split(", ");
                        getImageIcon(CateogoryLast[CateogoryLast.length - 1], holder.btnDelete);
                    }


                        getImageIcon(user.name,holder.btnDelete);

                if (user.OpenWebView == false){
                    holder.WebSummaryHTML.setVisibility(View.GONE);
                }
                    else
                {
                    holder.WebSummaryHTML.setVisibility(View.VISIBLE);

                }


                    Log.e("cache","3.18");


                    holder.imgReportIncorrect.setTag(R.string.RemediesShowHide, user.getCounter());
                    Log.e("cache","3.19");


				holder.textName.setText(user.getName());
                    holder.textName.setTag(user.getCategory());
				String sCounter = user.getCounter();
				Log.e("cache","3.20");
				Log.e("Getview","progress 10%");
				holder.btnDelete.setTag(R.string.ListPosition, position);
				holder.ratBar.setTag(R.string.ListPosition, position);
				holder.imgGallery.setTag(R.string.ListPosition, position);
                    holder.txtReviewInfo.setTag(R.string.ListPosition, position);
                holder.imgWebView.setTag(R.string.ListPosition, position);

                    holder.imgReportIncorrect.setTag(R.string.ListPosition, position);
                    Log.e("Getview","progress 20%");
				 //Log.e("getId check ", String.valueOf(user.getRemediesShowHide()));
				 int sRemedies = user.getRemediesShowHide();
				 Log.e("cache","3.21");
				 int iSublevel = user.getSubLevel();

                 String HtmlRem = user.getremedies();

                    holder.relSmartCommentItem.removeAllViews();
                    holder.objTimeHour.setVisibility(View.GONE);
                    holder.objPrice.setVisibility(View.GONE);
                    holder.imgCallButton.setVisibility(View.GONE);
                    holder.imgMedia.setVisibility(View.GONE);
                    holder.imgInternet.setVisibility(View.GONE);
                    holder.imgWebView.setVisibility(View.GONE);
                    holder.webSummary.setVisibility(View.GONE);
                    holder.WebSummaryHTML.setVisibility(View.GONE);
                    holder.imgGallery.setVisibility(View.GONE);

                    if (HtmlRem.equals("")){
                        holder.relSmartTabKent.setVisibility(View.GONE);
                        holder.txtReviewInfo.setVisibility(View.GONE); // when there is no address or just a category so comment will be invisible
                        holder.ratBar.setVisibility(View.GONE); //when there is no address or just a category so comment will be invisible
                    }
                    else
                    {

                        //   holder.txtRemedies.setText("");


                     //   holder.txtRemedies.setVisibility(View.VISIBLE);

                        String[] DetailsData = HtmlRem.split("--,--");

                        Log.e("cache","3.22");
                        String DetailsMsg = "";
                        boolean DetailFlyItemCreated = false;
                        for(int k=0; k <= DetailsData.length - 1; k++){
                            boolean SkipAddItem = false;
                            Log.e("cache","3.23");
                            String[] KeyValue = DetailsData[k].split("-,-");
                            if (KeyValue.length == 0) break;

                            LinearLayout layout = new LinearLayout(context);
                            RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams ( RelativeLayout.LayoutParams.WRAP_CONTENT,
                                    RelativeLayout.LayoutParams.WRAP_CONTENT );

                            layout.setOrientation(LinearLayout.HORIZONTAL);
                            iCounterForComment++;
                            layout.setId(iCounterForComment);
                            if ( iCounterForComment > 0 ) {
                                lp.addRule(RelativeLayout.BELOW, iCounterForComment - 1);
                            }

                            layout.setLayoutParams(lp);


                            Log.e("cache","3.25");

                            // final TextView textView = SLAc.FindItemNameForTextView(KeyValue[0]);
                            //textView =
                            //textView.setText(Html.fromHtml(KeyValue[1]), TextView.BufferType.SPANNABLE);
                            Log.e("DetailsMsg",DetailsMsg);
                            if (KeyValue[0].equalsIgnoreCase("Audio")  ){
                                final String URL = KeyValue[1];
                              //  textView.setText(Html.fromHtml("<font color='#4682B4'>Play/Stop Audio Note</font>"), TextView.BufferType.SPANNABLE);
                                holder.imgMedia.setVisibility(View.VISIBLE);
                                SkipAddItem = true;

                                holder.imgMedia.setTag("Play");
                                holder.imgMedia.setBackgroundResource(R.drawable.ic_youtube13);
                                holder.imgMedia.setOnClickListener(new OnClickListener() {

                                    @Override
                                    public void onClick(View v) {
                                        if (player != null){

                                            player.stop();
                                            player = null;

                                            holder.imgMedia.setBackgroundResource(R.drawable.ic_pause48);

                                        }
                                        if (holder.imgMedia.getTag().toString().indexOf("Play") !=  -1)
                                        try {
                                            player = new MediaPlayer();
                                            player.setAudioStreamType(AudioManager.STREAM_MUSIC);
                                            player.setDataSource("http://kent.nasz.us/app_php/shifaappsettings/mumbra/audio/"+ URL);
                                            player.prepare();
                                            player.start();
                                            holder.imgMedia.setTag("Stop");
                                         //   holder.imgMedia.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_stop, 0, 0, 0);
                                            holder.imgMedia.setBackgroundResource(R.drawable.ic_pause48);

                                        } catch (Exception e) {
                                            // TODO: handle exception
                                        }
                                        else
                                        {
                                            holder.imgMedia.setTag("Play");
                                            holder.imgMedia.setBackgroundResource(R.drawable.ic_youtube13);

                                        }

                                    }
                                });

                            }
                            else if (KeyValue[0].indexOf("Website") != -1 || KeyValue[0].indexOf("Email") != -1) {
                               // textView.setText(Html.fromHtml("<font color='#4682B4'>" + KeyValue[1] + " </font>"), TextView.BufferType.SPANNABLE);
                                holder.imgInternet.setVisibility(View.VISIBLE);
                                SkipAddItem = true;

                                final String URL = KeyValue[1];
                                holder.imgInternet.setOnClickListener(new OnClickListener() {

                                    @Override
                                    public void onClick(View v) {
                                        Intent i = new Intent(Intent.ACTION_VIEW);
                                        if (URL.toLowerCase().indexOf("www.") != -1) {
                                            String url = "";
                                            if (URL.startsWith("http://") || URL.startsWith("https://")){

                                            }else
                                            {
                                                if (!URL.startsWith("http://"))
                                                    url = "http://" + URL;

                                            }
                                            i.setData(Uri.parse(url));
                                        }
                                        else if (URL.toLowerCase().indexOf("www.") != -1) {
                                            Uri data = Uri.parse("mailto:"+URL);
                                            i.setData(data);
                                        }

                                        context.startActivity(i);

                                        }
                                });
                            }
                            else if (KeyValue[0].equalsIgnoreCase("Phone")) {
                                //textView.setText(Html.fromHtml("<font color='#2F4F4F'>" + KeyValue[1] + " </font>"), TextView.BufferType.SPANNABLE);
                                holder.imgCallButton.setVisibility(View.VISIBLE);
                                SkipAddItem = true;
                                final String URL = KeyValue[1];
                                holder.imgCallButton.setOnClickListener(new OnClickListener() {


                                    @Override
                                    public void onClick(View v) {


                                        String PhoneNoString = URL.toLowerCase().replace("+(91)","");
                                        PhoneNoString = PhoneNoString.toLowerCase().replace("+91","");
                                        PhoneNoString = PhoneNoString.toLowerCase().replace("(91)","");
                                        PhoneNoString = PhoneNoString.toLowerCase().replace("/",",");

                                        PhoneNoString = PhoneNoString.toLowerCase().replace("tel",",");
                                        PhoneNoString = PhoneNoString.toLowerCase().replace("mob",",");
                                        PhoneNoString = PhoneNoString.toLowerCase().replace("mobile",",");
                                        PhoneNoString = PhoneNoString.toLowerCase().replace("telephone",",");

                                        if (PhoneNoString.substring(0,1).equalsIgnoreCase(","))
                                        {
                                            PhoneNoString = PhoneNoString.substring(1);
                                        }
                                        PhoneNoString = PhoneNoString.toLowerCase().replaceAll("[^0-9,]","");
                                       // PhoneNoString = PhoneNoString.toLowerCase().replace("-","");
                                       // PhoneNoString = PhoneNoString.toLowerCase().replace(":","");
                                       // PhoneNoString = PhoneNoString.toLowerCase().replace(" ","");
                                       // PhoneNoString = PhoneNoString.toLowerCase().replace(".","");

                                    //    PhoneNoString = PhoneNoString.toLowerCase().replace(")","");
                                   //     PhoneNoString = PhoneNoString.toLowerCase().replace("(","");
                                      //  PhoneNoString = PhoneNoString.toLowerCase().replace(";",",");

                                      final  String[] ListPhoneNo = PhoneNoString.split(",");



                                        AlertDialog.Builder builder = new AlertDialog.Builder(context);
                                        builder.setTitle("Make your selection");
                                        builder.setItems(ListPhoneNo, new DialogInterface.OnClickListener() {
                                            public void onClick(DialogInterface dialog, int item) {
                                                // Do something with the selection
                                               // mDoneButton.setText(items[item]);

                                                try {
                                                    if(ListPhoneNo[item].equalsIgnoreCase("")) return;
                                                    Intent intent = new Intent(Intent.ACTION_CALL);
                                                    intent.setData(Uri.parse("tel:" + ListPhoneNo[item]));
                                                    context.startActivity(intent);
                                                }catch(Exception ex){
                                                    Toast.makeText(context.getApplicationContext(), "Number not in proper format or Device is not allowing to connect call..", 100).show();

                                                }
                                            }
                                        });
                                        AlertDialog alert = builder.create();
                                        alert.show();

                                    }
                                });
                            }
                            else if (KeyValue[0].equalsIgnoreCase("Time") || KeyValue[0].equalsIgnoreCase("Hours") ){
                               //Need to add logic to figure out is it open or close
                               // holder.objTimeHour.setText(KeyValue[1]);
                                holder.objTimeHour.setVisibility(View.VISIBLE);
                                SkipAddItem = true;
                            }
                            else if (KeyValue[0].equalsIgnoreCase("Html")){
                                holder.WebSummaryHTML.loadData(KeyValue[1], "text/html", "utf-8");

                                holder.imgWebView.setVisibility(View.VISIBLE);

                                SkipAddItem = true;
                                DetailFlyItemCreated=true;
                            }


                            else if (KeyValue[0].equalsIgnoreCase("Price Range") || KeyValue[0].equalsIgnoreCase("Cost")  || KeyValue[0].equalsIgnoreCase("Price")){
                                holder.objPrice.setText(KeyValue[1]);
                                holder.objPrice.setVisibility(View.VISIBLE);
                                SkipAddItem = true;
                            }
                            else{
                                if (!DetailsMsg.equals("")) DetailsMsg += "<br />";
                                DetailsMsg =  DetailsMsg + "<b><font color='#6585B5'>" + KeyValue[0] + ":</font></b> " +  KeyValue[1];
                                DetailFlyItemCreated=true;
                            }

                            //textView.setTextSize(context.getResources().getDimension(R.dimen.com_facebook_likebutton_text_size));
                            //textView.setTextAppearance(context, android.R.style.TextAppearance_Small);
                       //     textView.setPadding(1, 3, 0, 3);

                            Log.e("cache DetailsData",DetailsData[k]);
                            if (SkipAddItem == false) {
                              //  layout.addView(textView);
                                DetailFlyItemCreated = true;
                            }
                            Log.e("cache","3.26");

                            //holder.relSmartComment.addView(layout);
                        }
                        if (DetailFlyItemCreated == true){
                            holder.txtReviewInfo.setVisibility(View.VISIBLE); // when there is address that's mean not a category or just a category so comment will be visible
                            holder.ratBar.setVisibility(View.VISIBLE); // when there is address that's mean not a category or just a category so comment will be visible

                        }

                        holder.webSummary.setText(Html.fromHtml(DetailsMsg), TextView.BufferType.SPANNABLE);
                   //     user.OpenWebView = true;
                        holder.webSummary.setVisibility(View.VISIBLE);
                      /*  if (DetailsData.length == 1) {

                        }
                        else {
                           // holder.imgWebView.setVisibility(View.VISIBLE);
                        }*/
                    }




                    Log.e("EntryRead",SLAc.GetPreferenceValue("EntryRead"+user.name));
                    Log.e("Entry",Integer.valueOf(user.entry) + "");

                    if (user.entry.equals("0")){
					 holder.txtCounter.setVisibility(View.GONE);
                     holder.imgArrow.setVisibility(View.GONE);
				 }
                 else if (Integer.valueOf(SLAc.GetPreferenceValue("EntryRead"+user.getCategory() + "|" + user.getName())) != Integer.valueOf(user.entry)){
                     holder.txtCounter.setVisibility(View.VISIBLE);
                        holder.txtCounter.setBackgroundColor(context.getResources().getColor(R.color.c_orange));
                        holder.txtCounter.setText( user.entry);
                     holder.imgArrow.setVisibility(View.VISIBLE);
                 }
				 else
				 {
					 holder.txtCounter.setVisibility(View.VISIBLE);
                     holder.txtCounter.setBackgroundColor(context.getResources().getColor(R.color.c_green));
                     holder.txtCounter.setText( user.entry);
                     holder.imgArrow.setVisibility(View.VISIBLE);
                 }
			 	 Log.e("Getview","progress 100%" + user.entry);


                    Log.e("SmartEntit","here1");
                    Log.e("SmartEntit","here4");
                   // user.OpenImage = true;

                    ImageGenerate(holder,  user);



                    Log.e("SmartCategory","setLike "+"" + user.getSubLevel());


								if ( !user.getBook().equalsIgnoreCase(""))
								{


                                    int icountComment = 0;
                                    try{


                                        String ReadMeString = user.getBook();
										if (!ReadMeString.equals("")){ // Read Me

											String[] ReadMe = ReadMeString.toString().split("-:-");
											Log.e("SmartCategory"," ReadMe.length = " + ReadMe.length);


											holder.txtComment.setText(String.valueOf(ReadMe.length));
											holder.txtComment.setVisibility(View.VISIBLE);
                                            for(int k=0; k <= ReadMe.length; k++){
												String[] CommentDet = ReadMe[k].split("-,-");
												if (CommentDet.length == 0) break;
                                                icountComment++;
												LinearLayout layout = new LinearLayout(context);
												 RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams ( RelativeLayout.LayoutParams.WRAP_CONTENT,
												            RelativeLayout.LayoutParams.WRAP_CONTENT );

												layout.setOrientation(LinearLayout.HORIZONTAL);
												iCounterForComment++;
												layout.setId(iCounterForComment);
												if ( iCounterForComment > 0 ) {
													 lp.addRule(RelativeLayout.BELOW, iCounterForComment - 1);
											    }

												layout.setLayoutParams(lp);



												RelativeLayout.LayoutParams imageParams = new RelativeLayout.LayoutParams(50,50);
												ImageView imageView = new ImageView(context);
                                                imageView.setBackgroundResource(R.drawable.ic_action_officeworker2);

								    		    imageView.setLayoutParams(imageParams);
								    		    imageView.setPadding(5, 5, 5, 5);
								    		    layout.addView(imageView);

								    		    RelativeLayout.LayoutParams textParams = new RelativeLayout.LayoutParams( RelativeLayout.LayoutParams.WRAP_CONTENT,
											            RelativeLayout.LayoutParams.WRAP_CONTENT );
								    		    TextView textView = new TextView(context);
								    		    textView.setLayoutParams(textParams);
								    		    textView.setText(Html.fromHtml("<b><font color='#6585B5'>" + CommentDet[0].substring(0,4) + "XXXX" + CommentDet[0].substring(8,10) + " </font></b> " +CommentDet[1] + "<br /><font color='#888888'>"+CommentDet[2]+"</font>"), TextView.BufferType.SPANNABLE);
								    		    //textView.setTextSize(context.getResources().getDimension(R.dimen.com_facebook_likebutton_text_size));
								    		    textView.setTextAppearance(context, android.R.style.TextAppearance_Small);
								    		    textView.setPadding(5, 10, 5, 10);
								    		    layout.addView(textView);

								    		    holder.relSmartCommentItem.addView(layout);

											}

										}
									}
									catch(Exception ex){
										Log.e("SmartCategory","error " + ex.toString());
									}
                                    if (icountComment != 0) {
                                        holder.txtReviewInfo.setText(icountComment + " Review");
                                        holder.txtReviewInfo.setVisibility(View.VISIBLE); // when there is address that's mean not a category or just a category so comment will be visible
                                    }
                                    else{

                                        holder.txtReviewInfo.setVisibility(View.GONE); // when there is address that's mean not a category or just a category so comment will be visible
                                    }
                                    if (user.OpenComment == true) {
                                        holder.relSmartComment.setVisibility(View.VISIBLE);
                                    }
                                    else
                                    {
                                        holder.relSmartComment.setVisibility(View.GONE);
                                    }












                                }
                                else{ // If Comment aka Book is empty then hide RelSmartComment object invisible

                                    holder.relSmartComment.setVisibility(View.GONE);
                                    holder.txtReviewInfo.setVisibility(View.GONE); // when there is address that's mean not a category or just a category so comment will be visible

                                }



					 /*
					  * if (SLAc.jsonObject.getJSONObject(i).getString("title") == SmartCategoy + "|" + user.name);
						    {
						    	break;
						    }
						    for (int i = 0; i < SLAc.jsonObject.length(); i++) {
						 	JSONObject currObject = SLAc.jsonObject.getJSONObject(i);


						}
					 SLAc.jsonObject.get("title").toString();
					 */


				 holder.ratBar.setOnClickListener(new OnClickListener() {
						@Override
						public void onClick(View v) {
							/*int iTag = (Integer) v.getTag(R.string.ListPosition);
			                Log.e("iTag",String.valueOf(iTag));
			                User us = filterdata.get(iTag);

                            Intent intent = new Intent(context, activity_Kent_post_idea.class);
                            intent.putExtra("id_web", us.id_web);
                            context.startActivity(intent);
*/
                            UserHolder holder1 = (UserHolder)v.getTag(R.string.RemediesShowHide);
                            int iTag = (Integer) v.getTag(R.string.ListPosition);
                            User us = filterdata.get(iTag);
                            if(us.id_web == null){
                                Toast.makeText(context.getApplicationContext(), "Like will not work untill we review your entry or do update from menu option", 100).show();
                            }
                            else {
                                Log.e("iTag", String.valueOf(us.id_web));

                                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
                                nameValuePairs.add(new BasicNameValuePair("id_web", us.id_web));

                                Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/like.php", nameValuePairs, ((Activity) context));
                                Log.e("iTag", "done");
                                holder1.ratBar.setBackgroundResource(R.drawable.ic_thumbsup);

                            }
                        }
				 });

				 holder.imgGallery.setOnClickListener(new OnClickListener() {

						@Override
						public void onClick(View v) {
								int iTag = (Integer) v.getTag(R.string.ListPosition);
				                Log.e("iTag",String.valueOf(iTag));
				                User us = filterdata.get(iTag);
                        /*
				                Intent intent = new Intent(context, ImageGallery.class);
				                intent.putExtra("id_web", us.id_web);
                                 intent.putExtra("selected", us.selected);

								context.startActivity(intent);
								*/
                            UserHolder holder1 = (UserHolder)v.getTag(R.string.RemediesShowHide);
                            us.OpenImage = !us.OpenImage;
                            if (us.OpenImage == true) {
                                holder1.relSmartPicture.setVisibility(View.VISIBLE);
                            }
                            else
                            {
                                holder1.relSmartPicture.setVisibility(View.GONE);
                            }

                            ImageGenerate(holder1, us);




                        }
					});
                    holder.imgWebView.setOnClickListener(new OnClickListener() {

                        @Override
                        public void onClick(View v) {
                            int iTag = (Integer) v.getTag(R.string.ListPosition);
                            Log.e("iTag",String.valueOf(iTag));
                            User us = filterdata.get(iTag);
                        /*
				                Intent intent = new Intent(context, ImageGallery.class);
				                intent.putExtra("id_web", us.id_web);
                                 intent.putExtra("selected", us.selected);

								context.startActivity(intent);
								*/
                            UserHolder holder1 = (UserHolder)v.getTag(R.string.RemediesShowHide);
                            us.OpenWebView = !us.OpenWebView;
                            if (us.OpenWebView == true) {
                                holder1.WebSummaryHTML.setVisibility(View.VISIBLE);
                            }
                            else
                            {
                                holder1.WebSummaryHTML.setVisibility(View.GONE);
                            }





                        }
                    });
                    holder.txtReviewInfo.setOnClickListener(new OnClickListener() {

                        @Override
                        public void onClick(View v) {
                            int iTag = (Integer) v.getTag(R.string.ListPosition);
                            Log.e("iTag",String.valueOf(iTag));
                            User us = filterdata.get(iTag);
                        /*
				                Intent intent = new Intent(context, ImageGallery.class);
				                intent.putExtra("id_web", us.id_web);
                                 intent.putExtra("selected", us.selected);

								context.startActivity(intent);
								*/
                            UserHolder holder1 = (UserHolder)v.getTag(R.string.RemediesShowHide);
                            us.OpenComment = !us.OpenComment;
                            if (us.OpenComment == true) {
                                holder1.relSmartComment.setVisibility(View.VISIBLE);
                            }
                            else
                            {
                                holder1.relSmartComment.setVisibility(View.GONE);
                            }




                        }
                    });

                    holder.imgReportIncorrect.setOnClickListener(new OnClickListener() {

                        @Override
                        public void onClick(View v) {
                            int iTag = (Integer) v.getTag(R.string.ListPosition);
                            User us = filterdata.get(iTag);
                                Log.e("iTag", String.valueOf(us.id_web));

                                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
                                nameValuePairs.add(new BasicNameValuePair("id_web", us.id_web));
                                nameValuePairs.add(new BasicNameValuePair("categoy", us.getCategory()));
                                nameValuePairs.add(new BasicNameValuePair("Name",  us.getName()));
                                nameValuePairs.add(new BasicNameValuePair("session_id", us.Session_id));

                                Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/report.php", nameValuePairs, ((Activity) context));
                                Log.e("iTag", "done");
                                Toast.makeText(context.getApplicationContext(), "Thanks for reporting, We will review and update it on server. It will take sometime..", 100).show();



                        }
                    });
                   /* holder.imgBtnEditPost.setOnClickListener(new OnClickListener() {

                        @Override
                        public void onClick(View v) {
                            int iTag = (Integer) v.getTag(R.string.ListPosition);
                            final User us = filterdata.get(iTag);
                            Log.e("iTag", String.valueOf(us.id_web));


                            if (us.Session_id.equals("3233004756") || us.Counter.equals(us.Session_id)){
                                String URL = "http://kent.nasz.us/mumbra/php/webedit.php?id_web="+us.id_web;

                                Intent intent = new Intent(context, activity_event.class);
                                intent.putExtra("url", URL);
                                context.startActivity(intent);


                                return;
                            }


                            final AlertDialog.Builder alert = new AlertDialog.Builder(context);
                            final EditText input = new EditText(context);

                            input.setHint("Let us know what need to update?.. type here..");
                            alert.setTitle( "Edit Entry: " + us.getName() );
                            alert.setView(input);




                            alert.setPositiveButton("Send", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int whichButton) {

                                    String incorrectMsg = input.getText().toString().trim();
                                    final List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
                                    nameValuePairs.add(new BasicNameValuePair("id_web", us.id_web));
                                    nameValuePairs.add(new BasicNameValuePair("session_id", us.Session_id));
                                    nameValuePairs.add(new BasicNameValuePair("edit", incorrectMsg));

                                    Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/edit.php", nameValuePairs, ((Activity) context));
                                    Toast.makeText(context.getApplicationContext(), "Thanks for reporting. We will update it on server in next business day. It will take sometime..", 1000).show();

                                }
                            });

                            alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int whichButton) {
                                    dialog.cancel();
                                }
                            });

                            alert.show();




                        }
                    });
                    */



		return row;
				}
				catch(Exception exx)
				{
					Log.e("error",exx.toString());
					return row;

				}
	}

    private void ImageGenerate(UserHolder holder , User us){
        Log.e("ImageGenerate","ImageGenerate "  + us.getSelected());
        String PictureString = us.getSelected();
        PictureString = PictureString.replace(" ","");
        final String[] ImageArrayList = PictureString.split(",");
        Log.e("ImageGenerate","ImageGenerate "  + ImageArrayList.length);

        if (PictureString.equals("")) {
            holder.imgGallery.setVisibility(View.GONE);
        }
        else {
            holder.imgGallery.setVisibility(View.VISIBLE);
        }
        Log.e("ImageGenerate","OpenImage");
        if ( us.OpenImage == true ||  us.getSelected().indexOf("_set") != -1)
            holder.relSmartPicture.setVisibility(View.VISIBLE);
        else {
            holder.relSmartPicture.setVisibility(View.GONE);
            return;
        }
        Log.e("ImageGenerate","PictureString "+ PictureString);
        if (!PictureString.equals(""))
        {
            int iCounterForImage = 0;
            Display display = ((Activity) context).getWindowManager().getDefaultDisplay();
            Log.e("ImageGenerate","ImageGenerate");

            holder.xml_full_img_hor_below_view.removeAllViews();
                for(int i = 0; i <= ImageArrayList.length -1 ;i++) {
                    String ImageURL = ImageArrayList[i];
                  //  ImageURL = ImageURL.replace("_set","");
                    if (!ImageURL.equals("")) {
                        if (ImageURL.indexOf("_logo") != -1){


                        }
                        else {
                            iCounterForImage++;
                            ImageView imageView = new ImageView(context);
                            imageView.setTag(ImageURL);
                            imageView.setId(iCounterForImage);

                            //  int width =  ((display.getWidth()*40)/100);
                            // int height = ((display.getHeight()*40)/100);
                            // LinearLayout.LayoutParams parms = new LinearLayout.LayoutParams(width,height);
                            // imageView.setLayoutParams(parms);

                            imageView.setScaleType(ImageView.ScaleType.CENTER);
                            //   LinearLayout.LayoutParams params = (LinearLayout.LayoutParams)imageView.getLayoutParams();
                            //     params.setMargins(0, 0, 10, 0);
                            //    imageView.setPadding(10,0,10,0);
                            // imageView.setLayoutParams(params);


                            Log.e("ImageGenerate", "iCounterForImage " + iCounterForImage);

                            imageManager.displayImage(ImageURL + "_thumb", imageView, R.drawable.ic_screen_shot_2015_01_21_at_5);


                            imageView.setOnClickListener(new OnClickListener() {

                                @Override
                                public void onClick(View v) {
                                    String sUrl = (String) v.getTag();
                         /*   File cacheDir;
                            String sdState = android.os.Environment.getExternalStorageState();
                            if (sdState.equals(android.os.Environment.MEDIA_MOUNTED)) {
                                File sdDir = android.os.Environment.getExternalStorageDirectory();
                                cacheDir = new File(sdDir,"data/shifapics");

                            } else {
                                cacheDir = context.getCacheDir();
                            }*/
                                    // Intent i = new Intent(Intent.ACTION_VIEW);
                                    // context.startActivity(i);
                                    Intent intent = new Intent(context, ImageViewFullScreen.class);
                                    intent.putExtra("imageUrl", sUrl);
                                    context.startActivity(intent);


                                }
                            });
                            holder.xml_full_img_hor_below_view.addView(imageView);


                            ImageView divider = new ImageView(context);
                            LinearLayout.LayoutParams lp =
                                    new LinearLayout.LayoutParams(android.widget.AbsListView.LayoutParams.MATCH_PARENT, AbsListView.LayoutParams.WRAP_CONTENT);
                            lp.setMargins(10, 0, 10, 0);
                            divider.setLayoutParams(lp);
                            divider.setBackgroundColor(Color.RED);
                            holder.xml_full_img_hor_below_view.addView(divider);
                        }
                    }
                }
                    holder.xml_full_img_hor_below_view.setVisibility(View.VISIBLE);



        }
        else{
            //  holder.imgPost.setVisibility(View.GONE);
            holder.xml_full_img_hor_below_view.setVisibility(View.GONE);
            holder.txtImageCount_l_r_d.setText("");
        }

    }
	/**
	���������* Implementing the Filterable interface.
	���������*/
	private String RestoreShifaConnectType()
	{
		SharedPreferences prefs = context.getSharedPreferences("AppNameSettings",0);
		String restoredText = prefs.getString("SaveShifaConnectType", null);
		if (restoredText != null)
		{
			return restoredText;
		}
		return "";

	}
	public String getColorOnRemedies(String r)
	{

	    //window.MyCls.log(remedies);
	    return "";
	}
	static class UserHolder {
		TextView textName;
		TextView txtCounter;
		ImageView btnDelete;
		TextView txtLike;
		TextView txtComment;
		ImageView imgReportIncorrect;
		TextView txtReadMe;
		ImageView imgPost;
        LinearLayout xml_full_img_hor_below_view;
		RatingBar ratBar;
		ImageView imgReportViewShortCut;
        ImageButton imgCallButton;
        HorizontalScrollView relSmartPicture;
        TextView txtImageCount_l_r_d;
        ImageView imgArrow;
        RelativeLayout relSmartTabKent;
		LinearLayout relSmartComment;
        RelativeLayout relSmartCommentItem;
        TextView objTimeHour;
        TextView objPrice;
        ImageButton imgMedia;
        ImageButton imgGallery;
        ImageButton imgInternet;
        TextView txtReviewInfo;
        TextView webSummary;
        ImageButton imgWebView;
        WebView WebSummaryHTML;
	}
	@Override
    public Filter getFilter() {
		  return new Filter() {
			  @Override
		        protected FilterResults performFiltering(CharSequence constraint) {
		             FilterResults oReturn = new FilterResults();
		             ArrayList<User> results = new ArrayList<User>();
		          
		            ArrayList<User> orig = new ArrayList<User>();
		            Log.e("orig","empty");
		           
		            	Log.e("orig","true");
		            	orig = data;
		            	Log.e("orig","false");
		            	
		            Log.e("orig",String.valueOf(orig.size()));
		            if (constraint != null) {
		                if (orig != null && orig.size() > 0) {
		                    for (final User g : orig) {
		                    	String[] sWords = constraint.toString().split(" ");
		                    	int iMatched = 0;
		                    	for (String sWrd : sWords) {
		                    		
		                    		Log.e("sWrd ", sWrd);
		                    		if (g.getName().toLowerCase()
			                                .contains(sWrd.toLowerCase().toString()))
			                        {
		                    			iMatched++;
			                        	

			                        }
			                    	else if (g.getCategory().toLowerCase()
			                                .contains(sWrd.toLowerCase().toString()))
			                        {
			                    		iMatched++;
			                        	
				                    	    
			                        }
                                    else if (g.getremedies().toLowerCase()
                                            .contains(sWrd.toLowerCase().toString()))
                                    {
                                        iMatched++;


                                    }
			                    	
		                    		
		                    	}
		                    	Log.e("sWrd.length()", String.valueOf(sWords.length));
	                    		Log.e("iMatched ", String.valueOf(iMatched));
	                    		if (iMatched == sWords.length )
	                    		{
		                        	
		                    	    results.add(g);

	                    		}
		                    	
		                    }
		                }
		                oReturn.count = results.size();
		                oReturn.values = results;
		            }
		            return oReturn;
		        }

		        @SuppressWarnings("unchecked")
		        @Override
		        protected void publishResults(CharSequence constraint,
		                FilterResults results) {
		        	
		        	  ArrayList<User> items;
		        	  Log.e("orig","notifyDataSetChanged");
		        	  
		            items = (ArrayList<User>) results.values;
		           
		            Log.e("orig","notifyDataS items etChanged");
		            notifyDataSetChanged(items);
		        }
		    };
		}
    private void getImageIcon(String caption, ImageView img){
        String cap = caption.replaceAll(" ","_");
        if (caption.equalsIgnoreCase(MyAccountName))
            img.setBackgroundResource(R.drawable.ic_contact_icon);
        if (caption.indexOf("SMS To Mumbra") != -1)
            img.setImageResource(R.drawable.ic_sms);
        else if (cap.equalsIgnoreCase("apparels") || cap.equalsIgnoreCase("cloths"))

            img.setImageResource(R.drawable.ic_icon_merchandise_clothes);
        else if (cap.equalsIgnoreCase("atms"))

            img.setImageResource(R.drawable.ic__atm);
        else if (cap.equalsIgnoreCase("baby_school"))

            img.setImageResource(R.drawable.ic__baby_school);
        else if (cap.equalsIgnoreCase("bank"))

            img.setImageResource(R.drawable.ic__bank);

        else if (cap.equalsIgnoreCase("books"))

            img.setImageResource(R.drawable.ic__book);

        else if (cap.equalsIgnoreCase("church"))

            img.setImageResource(R.drawable.ic__church);

        else if (cap.equalsIgnoreCase("chemists"))

            img.setImageResource(R.drawable.ic__chemist);

        else if (cap.equalsIgnoreCase("Jobs"))

            img.setImageResource(R.drawable.ic_jobs);
        else if (cap.equalsIgnoreCase("company"))

            img.setImageResource(R.drawable.ic_ic_office_building);

        else if (cap.equalsIgnoreCase("classes"))

            img.setImageResource(R.drawable.ic__classes);
        else if (cap.equalsIgnoreCase("classes"))

            img.setImageResource(R.drawable.ic__classes);
        else if (cap.equalsIgnoreCase("buildings"))

            img.setImageResource(R.drawable.ic_companies);

        else if (cap.equalsIgnoreCase("computers"))

            img.setImageResource(R.drawable.ic__computer);

        else if (cap.equalsIgnoreCase("ngo"))

            img.setImageResource(R.drawable.ic__ngo);
        else if (cap.equalsIgnoreCase("gas"))

            img.setImageResource(R.drawable.ic__gas);

        else if (cap.equalsIgnoreCase("grocery"))

            img.setImageResource(R.drawable.ic_grocery);
        else if (cap.equalsIgnoreCase("gym"))

            img.setImageResource(R.drawable.ic__gym);

        else if (cap.equalsIgnoreCase("buy_or_sell"))

            img.setImageResource(R.drawable.ic_buy_sell_transparent);

        else if (cap.equalsIgnoreCase("mobile"))

            img.setImageResource(R.drawable.ic__mobile_shop);

        else if (cap.equalsIgnoreCase("motor_learning"))

            img.setImageResource(R.drawable.ic__motor_learning);

        else if (cap.equalsIgnoreCase("office"))

            img.setImageResource(R.drawable.ic__office);
        else if (cap.equalsIgnoreCase("News"))

            img.setImageResource(R.drawable.ic_ic__news);


        else if (cap.equalsIgnoreCase("pizza"))

            img.setImageResource(R.drawable.ic__pizza);

        else if (cap.equalsIgnoreCase("real_estate"))

            img.setImageResource(R.drawable.ic__real_estate);

        else if (cap.equalsIgnoreCase("restaurants"))

            img.setImageResource(R.drawable.ic_547b46f90507e7206e16bca4_restaurantmenuicon);

        else if (cap.equalsIgnoreCase("Police"))

            img.setImageResource(R.drawable.ic__security);

        else if (cap.equalsIgnoreCase("sports"))

            img.setImageResource(R.drawable.ic__sports);

        else if (cap.equalsIgnoreCase("station"))

            img.setImageResource(R.drawable.ic__station);
        else if (cap.equalsIgnoreCase("Train"))

            img.setImageResource(R.drawable.ic__station);


        else if (cap.equalsIgnoreCase("wedding") || cap.equalsIgnoreCase("hall") || cap.equalsIgnoreCase("marriage_hall"))

            img.setImageResource(R.drawable.ic__wedding);

        else if (cap.equalsIgnoreCase("xerox"))

            img.setImageResource(R.drawable.ic__xerox);


        else if (cap.equalsIgnoreCase("Temple"))

            img.setImageResource(R.drawable.ic__temple);
        else if (cap.equalsIgnoreCase("Tour_&_Travel"))

            img.setImageResource(R.drawable.ic__tour);
        else if (cap.equalsIgnoreCase("Masjid"))

            img.setImageResource(R.drawable.ic__masjid);

        else if (cap.equalsIgnoreCase("Software") || cap.equalsIgnoreCase("laptop"))

            img.setImageResource(R.drawable.ic__computer);


        else if (cap.equalsIgnoreCase("School_&_College"))

            img.setImageResource(R.drawable.ic__school);


    }
		public void notifyDataSetChanged(ArrayList<User> performfilter) {
		    super.notifyDataSetChanged();
		    Log.e("runnable","action");
		    this.filterdata = performfilter;
		    Log.e("runnable","action end");
		   // notifyChanged = true;
		}
		public String getRemediesColor(String r, String book)
		{
			Log.e("getRemediesColor", book);

			if (book.equalsIgnoreCase("Boenninghausens")){
				return getRemediesBoenninghausens(r);
			}
			else
			{
				return getRemediesKent(r);
			}
			
		}
		public String getRemediesKent(String r)
		{
		    try
		    {
                return r;
			  /*  if (r == "" || r == null) return "-";
			    String[] remS = r.split(":");
			    Log.e("remS",String.valueOf(remS.length));
			    String remedies = " ";
			    //window.MyCls.log("remedies 1 remS.length" + remS.length) ;
			    String str = "";
			    for(int i=0;i<= remS.length-1;i++)
			    {
			        String[] spi = remS[i].split(",");
			        if (spi[1].equals("1"))
			        {
			            str = "<font color='black'>" + spi[0] + "</font>, ";
			        }
			        else if (spi[1].equals("2"))
			        {
			            str = "<font color='blue'>" + spi[0] + "</font>, ";
			        }
			        else if (spi[1].equals("3"))
			        {
			            str = "<font color='red'>" + spi[0] + "</font>, ";
			        }
			        remedies = remedies + str;*/
			        
			    //}
			   // return remedies;
		    }
		    catch(Exception ex)
		    {
		    	return "-";
		    }
		    //window.MyCls.log(remedies);
		    
		}
		public String getRemediesBoenninghausens(String r)
		{
			Log.e("getRemediesBoenninghausens", "I am in");
		    try
		    {
			    if (r == "" || r == null) return "-";
			    String[] remS = r.split(":");
			    Log.e("remS",String.valueOf(remS.length));
			    String remedies = " ";
			    //window.MyCls.log("remedies 1 remS.length" + remS.length) ;
			    String str = "";
			    for(int i=0;i<= remS.length-1;i++)
			    {
			        String[] spi = remS[i].split(",");
			        if (spi[1].equals("1"))
			        {
			            str = "<font color='black'>" + spi[0] + "</font>, ";
			        }
			        else if (spi[1].equals("2"))
			        {
			            str = "<font color='#009933'>" + spi[0] + "</font>, ";
			        }
			        else if (spi[1].equals("3"))
			        {
			            str = "<font color='blue'><i>" + spi[0] + "</i></font>, ";
			        }
			        else if (spi[1].equals("4"))
			        {
			            str = "<font color='red'><b>" + spi[0] + "</b></font>, ";
			        }
			        else if (spi[1].equals("5"))
			        {
			            str = "<font color='#000080'><u><b>" + spi[0] + "</b></u></font>, ";
			        }
			        remedies = remedies + str;
			        
			    }
			    return remedies;
		    }
		    catch(Exception ex)
		    {
		    	return "-";
		    }
		    //window.MyCls.log(remedies);
		    
		}
		
		
		
	
}



