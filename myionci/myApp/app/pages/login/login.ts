import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
// Import Pages to use
import {LoginVerifyPage} from '../loginverify/loginverify';
// Import Tabs handler
import {TabsPage} from '../tabs/tabs';
// Import Footer handler
import {MoreShared} from '../moreshared/moreshared';
// Import Authentication handler
import {Authentication} from '../authentication/authentication';


@Page({
  templateUrl: 'build/pages/login/login.html',
  providers: [MoreShared, Authentication]
})
export class LoginPage {
  // Definitions
  public loginForm: any;        // Login form info.
  public moreshared: any;       // Shared handler for footer elements

  constructor(private nav: NavController, fb: FormBuilder, moreshared: MoreShared, public auth: Authentication) {
    this.loginForm = fb.group({
      countrycode: ['', Validators.required],
      mobile: ['', Validators.required]
    });
    this.moreshared = moreshared;

  }


   // Handle fabric phone login
  handleDigitsLogin() {
    console.log('handleDigitsLogin');
    this.nav.push(LoginVerifyPage);
  }

  // Handle Facebook login
  handleFacebookLogin() {
    console.log('handleFacebookLogin');
    this.auth.signInWithFacebook()
      .then(
        function(response) {
          console.log(JSON.stringify(response);
        };
        this.nav.push(TabsPage);
      );
  }

   // Handle twitter login
  handleTwitterLogin() {
    console.log('handleTwitterLogin');
    this.auth.signInWithTwitter()
      .then(
          function(response) {
            console.log(JSON.stringify(response);
          };
        this.nav.push(TabsPage);
      );
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
}