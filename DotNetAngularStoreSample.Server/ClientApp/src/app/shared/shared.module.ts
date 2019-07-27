import { NgModule } from "@angular/core";

import { MaterialModule } from "./material.module";
import { SpinnerComponent } from "./components/spinner/spinner.component";
import { ErrorComponent } from "./components/error/error.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

@NgModule({
  imports: [
    FormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule
  ],
  declarations: [SpinnerComponent, ErrorComponent, NotFoundComponent],
  exports: [MaterialModule, SpinnerComponent, ErrorComponent, NotFoundComponent]
})
export class SharedModule {}
