import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/rewardsterms/rewardsterms.html',
})
export class RewardsTermsPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}
