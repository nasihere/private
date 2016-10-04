import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'choosechallenger',
  templateUrl: 'build/components/choosechallenger/choosechallenger.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class chooseChallenger {
  constructor() {
  }

}
