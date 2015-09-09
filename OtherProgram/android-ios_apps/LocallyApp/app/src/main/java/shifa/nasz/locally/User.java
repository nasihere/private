package shifa.nasz.locally;

public class User {
	  private final Object mLock = new Object();
	 String name;
	 String category;
	 String Counter = "0";
	 String remedies;
	 String selected;
	 int sublevel;
	 int id;
	 int RemediesShowHide = 1;
     String book;
	 String title;
    boolean OpenImage = false;
    boolean OpenComment = false;
    boolean OpenWebView = false;
    String entry;
    String lat_lng;
    String id_web;
    String Session_id;
	 String Json;
	 public String getBook() {
	  return book;
	 }

	 public void setBook(String book) {
	  this.book = book;
	 }

		 
	 public String getName() {
	  return name;
	 }

	 public void setName(String name) {
	  this.name = name;
	 }

	 public String getremedies() {


             return remedies;


     }

		 public void setremedies(String remedies) {
		  this.remedies= remedies;
		 }

		 
	 public String getCategory() {
		 if (category == null) return "";
         category = category.replace("|", ", ");
		 
	  return category;
	 }

	 public void setAddress(String address) {
		 
		 this.category = address;
	 }

	 public String getCounter() {
		 return Counter;
	 }

	 public void setCounter(String Counter) {
		 
	  this.Counter = Counter;
	 }

	 public int getRemediesShowHide() {
	  return RemediesShowHide;
	 }

	 public void setRemediesShowHide(int RemediesShowHide) {
	  this.RemediesShowHide= RemediesShowHide;
	 }
	 

	 public int getSubLevel() {
	  return sublevel;
	 }

	 public void setSubLevel(int SubLevel) {
	
	  this.sublevel= SubLevel;
	 }

	 
	 public int getID() {
		  return id;
		 }

		 public void setID(int id) {
		
		  this.id= id;
		 }
		 

		 public String getSelected() {
			 if (selected == null) return "0";
			  return selected;
			 }

			 public void setSelected(String Selected) {
			
			  this.selected= Selected;
			 }
		
			 
	 public User(String name, String category, String Counter, String remedies, int sublevel, int id, String Selected,String book, String title, String json, String id_web, String entry, String Session_id, String lat_lng) {
	  super();
	  this.name = name;
	  this.id= id;
	  this.category = category;
	  this.Counter = Counter;
	  this.remedies = remedies;
	  this.sublevel = sublevel;
	  this.selected = Selected;
	  this.book = book;
	  this.title = title;
	  this.Json = json;
         this.id_web = id_web;
         this.entry = entry;
         this.Session_id = Session_id;
         this.lat_lng = lat_lng;
	 }

	 
	}