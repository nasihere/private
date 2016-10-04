import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES, NavController} from 'ionic-angular';
import {CreateChallengePage} from '../../pages/createchallenge/createchallenge';

@Component({
  selector: 'homeinfobar',
  templateUrl: 'build/components/homeinfobar/homeinfobar.html',
  directives: [IONIC_DIRECTIVES]
})
export class Homeinfobar {
  constructor(private nav: NavController) {
  }

  // Handle the Create a Challenge button
  handleCreateAChallenge() {
    console.log('handleCreateAChallenge');
    this.nav.push(CreateChallengePage);
  }

}
