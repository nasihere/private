import {Page, NavController, NavParams, Modal, ViewController} from 'ionic-angular';
import {ProfilePage} from '../profile/profile';
import {HomePage} from '../home/home';
import {MoreShared} from '../moreshared/moreshared';
// Import Authentication handler
import {Authentication} from '../authentication/authentication';

@Page({
  templateUrl: 'build/pages/tab-more/tab-more.html',
  providers: [MoreShared, Authentication]
})
export class TabMoreModal {
  // Declarations
  public moreshared: any;
  public moretabposition: number = 4;         // More tab position
  public notifications: number = 0;           // Number of notifications outstanding
  public notificationstxt: string = '';

  constructor(private nav: NavController,
              private viewCtrl: ViewController,
              moreshared: MoreShared,
              private auth: Authentication) {
    this.viewCtrl = viewCtrl;
    this.moreshared = moreshared;

    // Set the number of notifications
    this.setNotificationCount();
  }

  // Close the more view
  close() {
    // Unset the More modal background/hilite
    this.unhiliteMoreTab();
    // Close the more dialog
    this.viewCtrl.dismiss();
  }

  // Set the number of notifications
  setNotificationCount() {
    // Setup
    this.notificationstxt = '';
    // Get the notification count
    this.notifications = 0;
    // Set the menu element
    if (this.notifications > 0) {
      this.notificationstxt = String(this.notifications);
    }
  }

  // Unhilight the More tab
  unhiliteMoreTab() {
    document.getElementById('navtabs')
      .getElementsByTagName('tabbar')[0]
      .getElementsByTagName('a')[this.moretabposition]
      .setAttribute('aria-selected','false');
  }

  // Handle the close event
  preventClose(event) {
    event.stopPropagation();
    //console.log(event);
  }

  // Logout and go to the home page
  logOut() {
    // Logout from Firebase
    this.auth.signOut();
    // Go to the Home Page
    this.nav.setRoot(HomePage);
  }

  // Go to profile page
  goToProfile() {
    this.nav.push(ProfilePage);
  }

  // Go to Notificatons page
  goToNotifications() {
    console.log('goToNotifications');
  }

  // Go to Settings page
  goToSettings() {
    console.log('goToSettings');
  }

  // Handle going to the FAQ view
  openFAQ() {
    this.moreshared.openFAQ();
  }

  // Handle going to the TOS view
  openTOS() {
    this.moreshared.openTOS();
  }

  // Handle going to the Privacy view
  openPrivacy() {
    this.moreshared.openPrivacy();
  }

  // Handle going to the Rewards Terms view
  openRewardsTerms() {
    this.moreshared.openRewardsTerms();
  }

  // Handle going to the About Us view
  openAboutUs() {
    this.moreshared.openAboutUs();
  }

  // Handle going to the Contact Us view
  openContactUs() {
    this.moreshared.openContactUs();
  }

}