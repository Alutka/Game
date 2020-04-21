import {createDocumentFragmentFromString} from "../utils/utils";

export class ProgressBar {
    private id: string;
    private iconClassname: string;
    private min: number;
    private max: number;
    private value: number;
    private color: string;
    private progressBar: DocumentFragment;
    private valueContainer: HTMLElement;

    constructor(id: string, iconClassname: string, color: string, value: number, min: number, max: number) {
        this.id = `${id}-progress-bar`;
        this.iconClassname = iconClassname;
        this.value = value;
        this.min = min;
        this.max = max;
        this.color = color;
    }

    public display() {
        const container = document.getElementById("progress_bars");
        const progressBarWrapper = document.createElement("div");
        progressBarWrapper.classList.add("progress_bar_wrapper");
        progressBarWrapper.id = `${this.id}-wrapper`;
        const infoContainer = document.createElement("div");
        infoContainer.classList.add("info");
        infoContainer.appendChild(createDocumentFragmentFromString(`<i class="${this.iconClassname}"></i>`));
        progressBarWrapper.appendChild(infoContainer);
        this.valueContainer = document.createElement("div");
        this.valueContainer.innerText=`${this.value}/${this.max}`;
        this.valueContainer.classList.add("value");
        infoContainer.appendChild(this.valueContainer);
        this.progressBar = createDocumentFragmentFromString(getProgressBarTemplate(this.id, this.value, this.min, this.max));
        progressBarWrapper.appendChild(this.progressBar);
        container.appendChild(progressBarWrapper);
        this.setValue(this.value);
        this.setBarColor(this.color);
        return this;
    }

    private setBarColor(color?: string){
        if(color!==undefined) {
            this.color = color;
        }
        document.getElementById(this.id).style.backgroundColor = color;
        return this;
    }

    private setValue(value: number){
        if(value!==undefined){
            this.value = value;
        }
        document.getElementById(this.id).style.width = `${Math.min(100,100*this.value/(this.max-this.min))}%`;
        return this;
    }

    private setMin(min?: number){
        if(min!==undefined){
            this.min = min;
        }
        return this;
    }

    private setMax(max?:number){
        if(max!==undefined){
            this.max = max;
        }
        return this;
    }

    public update(value: number, min?: number, max?: number, color?: string) {
        this.setMin(min);
        this.setMax(max);
        this.setValue(value);
        this.progressBar = createDocumentFragmentFromString(getProgressBarTemplate(this.id, this.value, this.min, this.max));
        this.valueContainer.innerHTML=`${this.value}/${this.max}`;
        this.setBarColor(color);
        return this;
    }
}

function getProgressBarTemplate(id: string, value: number, min: number, max: number): string {
    return `<div class="progress">
  <div id=${id} class="progress-bar" role="progressbar" aria-valuenow=${value} aria-valuemin=${min} aria-valuemax=${max}></div>
</div>`
}

const progressBar1 = new ProgressBar("food", "icon fas fa-utensils", "yellow", 50, 0, 100).display();
const progressBar2 = new ProgressBar("carriage", "icon fas fa-weight-hanging", "yellow", 70, 0, 100).display();
setTimeout(()=>{
    progressBar2.update(60, 0, 200, "pink")
},3000);
const progressBar3 = new ProgressBar("effort", "icon fas fa-hammer", "yellow", 7.22, 0, 12).display();