let mainEl = document.querySelector('.logos');

let centerEl = document.getElementsByClassName('center-of-circle')[0];

let firstLogoEl = document.getElementById('first');
let secondLogoEl = document.getElementById('second');
let thirdLogoEl = document.getElementById('third');
let forthLogoEl = document.getElementById('forth');
let fifthLogoEl = document.getElementById('fifth');
let sixthLogoEl = document.getElementById('sixth');
let seventhLogoEl = document.getElementById('seventh');
let eightLogoEl = document.getElementById('eight');

generateResponsiveElements(
    mainEl,
    centerEl,
    firstLogoEl,
    secondLogoEl,
    thirdLogoEl,
    forthLogoEl,
    fifthLogoEl,
    sixthLogoEl,
    seventhLogoEl,
    eightLogoEl
);

function generateResponsiveElements(
    mainEl,
    centerEl,
    firstLogoEl,
    secondLogoEl,
    thirdLogoEl,
    forthLogoEl,
    fifthLogoEl,
    sixthLogoEl,
    seventhLogoEl,
    eightLogoEl) {
    let lh = mainEl.offsetHeight;
    let ch = centerEl.offsetHeight;
    let smh = fifthLogoEl.offsetHeight;

    let cordinates = cordinatesOfCornerLogos(lh, smh);

    centerEl.style.left = `${(lh) / 2 - (ch) / 2}px`;
    centerEl.style.top = `${(lh) / 2 - (ch) / 2}px`;

    firstLogoEl.style.left = `${(lh) / 2 - (smh) / 2}px`;
    firstLogoEl.style.top = `-${smh / 2}px`;

    secondLogoEl.style.left = `${(lh) / 2 - (smh) / 2}px`;
    secondLogoEl.style.top = `${lh - (smh) / 2}px`;

    thirdLogoEl.style.left = `${lh - (smh) / 2}px`;
    thirdLogoEl.style.top = `${(lh) / 2 - (smh) / 2}px`;

    forthLogoEl.style.left = `-${smh / 2}px`;
    forthLogoEl.style.top = `${(lh) / 2 - (smh) / 2}px`;

    fifthLogoEl.style.left = `${cordinates.c1}px`;
    fifthLogoEl.style.top = `${cordinates.c1}px`;

    sixthLogoEl.style.left = `${cordinates.c2}px`;
    sixthLogoEl.style.top = `${cordinates.c1}px`;

    seventhLogoEl.style.left = `${cordinates.c1}px`;
    seventhLogoEl.style.top = `${cordinates.c2}px`;

    eightLogoEl.style.left = `${cordinates.c2}px`;
    eightLogoEl.style.top = `${cordinates.c2}px`;
}

let logoElements = Array.from(document.getElementsByClassName('logo'));

logoElements.forEach((logo) => {
    logo.addEventListener('mouseover', (e) => {
        manageRotation(logo.parentElement);

        Array
            .from(logo.parentElement.children)
            .forEach(el => manageRotation(el));

        manageSize(logo, 100);
    });

    logo.addEventListener('mouseout', (e) => {
        manageRotation(logo.parentElement);

        Array
            .from(logo.parentElement.children)
            .forEach(el => manageRotation(el));

        manageSize(logo, 80);
    });
})

function manageRotation(element) {
    if (element.style.animationPlayState != 'paused') element.style.animationPlayState = 'paused';
    else element.style.animationPlayState = 'running';
}

function manageSize(element, size) {

    element.children[0].style.height = `${size}px`;
    element.children[0].style.width = `${size}px`;
}

function cordinatesOfCornerLogos(lh, smh) {
    let c1 = ((lh - (lh / Math.sqrt(2))) - smh) / 2;
    let c2 = lh - (smh + c1);

    return { c1, c2 };
}