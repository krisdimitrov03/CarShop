let extrasSelect = document.querySelector('#extras-select');

Array.from(document.querySelectorAll('.extra input')).forEach(el => {
	el.addEventListener('change', (e) => {
		if (e.currentTarget.checked) {
			let optionEl = document.createElement('option');
			optionEl.text = e.currentTarget.nextElementSibling.textContent;
			optionEl.value = e.currentTarget.value;
			optionEl.selected = true;

			extrasSelect.appendChild(optionEl);
		} else {
			let elementToRemove = Array
				.from(extrasSelect.children)
				.find(el => el.value == e.currentTarget.value);

			extrasSelect.removeChild(elementToRemove);
		}
	})
})