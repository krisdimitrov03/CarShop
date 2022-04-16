﻿let roles = [];

let rolesSelect = document.getElementById('roles-select');

if (rolesSelect.children !== null) {
	Array.from(rolesSelect.children).forEach(optionEl => {
		if (optionEl.selected === true) {
			roles.push(optionEl.value);
        }
	});
}

Array.from(document.querySelectorAll('.role input')).forEach(roleEL => {
	if (roles.includes(roleEL.value)) {
		roleEL.checked = true;
	}
})

Array.from(document.querySelectorAll('.role input')).forEach(el => {
	el.addEventListener('change', (e) => {
		if (e.currentTarget.checked) {
			let optionEl = document.createElement('option');
			optionEl.text = e.currentTarget.value;
			optionEl.value = e.currentTarget.value;
			optionEl.selected = true;

			rolesSelect.appendChild(optionEl);
		} else {
			let elementToRemove = Array
				.from(rolesSelect.children)
				.find(el => el.value == e.currentTarget.value);

			rolesSelect.removeChild(elementToRemove);
		}
	})
})