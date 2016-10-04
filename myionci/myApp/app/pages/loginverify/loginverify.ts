import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
// Import Tabs handler
import {TabsPage} from '../tabs/tabs';
// Import Footer handler
import {MoreShared} from '../moreshared/moreshared';

@Page({
  templateUrl: 'build/pages/loginverify/loginverify.html',
  providers: [MoreShared]
})
export class LoginVerifyPage {
  // Definitions
  public loginVerifyForm: any;        // Login Verification form info.
  public moreshared: any;             // Shared handler for footer elements

  constructor(private nav: NavController, fb: FormBuilder, moreshared: MoreShared) {
    this.loginVerifyForm = fb.group({
      code: ['', Validators.required]
    });
    this.moreshared = moreshared;
  }

  // Handle fabric phone login verification
  handleDigitsVerify() {
    console.log('handleDigitsVerify');
    this.nav.push(TabsPage);
  }

  // Handle a fabric resend of the code
  handleResendCode() {
    console.log('handleResendCode');
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