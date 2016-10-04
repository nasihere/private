import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/privacypolicy/privacypolicy.html',
})
export class PrivacyPolicyPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}
