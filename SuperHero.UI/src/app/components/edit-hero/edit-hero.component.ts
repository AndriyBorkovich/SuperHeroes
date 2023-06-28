import { Component, Input, Output,EventEmitter, OnInit } from '@angular/core';
import { SuperHero } from 'src/app/models/super-hero';
import { SuperHeroService } from 'src/app/services/super-hero.service';

@Component({
  selector: 'app-edit-hero',
  templateUrl: './edit-hero.component.html',
  styleUrls: ['./edit-hero.component.css']
})
export class EditHeroComponent implements OnInit {
  // get data from form and save it in hero
  @Input() hero?: SuperHero;

  // its like new event
  @Output() heroesUpdated = new EventEmitter<SuperHero[]>();

  // inject service
  constructor(private superHeroService: SuperHeroService) {}

  ngOnInit(): void {
    
  }

  updateHero(hero: SuperHero) {
    this.superHeroService.updateHero(hero).subscribe((heroes: SuperHero[]) => this.heroesUpdated.emit(heroes));
  }

  deleteHero(hero: SuperHero) {
    this.superHeroService.deleteHero(hero).subscribe((heroes: SuperHero[]) => this.heroesUpdated.emit(heroes));
  }

  createHero(hero: SuperHero) {
    this.superHeroService.createHero(hero).subscribe((heroes: SuperHero[]) => this.heroesUpdated.emit(heroes));
  }
}
