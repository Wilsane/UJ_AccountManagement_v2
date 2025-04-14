import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsMainComponent } from './accounts-main.component';

describe('AccountsMainComponent', () => {
  let component: AccountsMainComponent;
  let fixture: ComponentFixture<AccountsMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccountsMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountsMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
