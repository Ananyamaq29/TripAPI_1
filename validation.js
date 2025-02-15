document.addEventListener("DOMContentLoaded", function() {
    const form = document.getElementById('travel-form');
    const destination = document.getElementById('destination');
    const startDate = document.getElementById('startDate');
    const endDate = document.getElementById('endDate');
    const preferences = document.getElementById('preferences');
    const comments = document.getElementById('comments');
    const submitButton = document.getElementById('submit-button');
    const errorMessage = document.getElementById('error-message');
    const summarySection = document.getElementById('summary');
    
    // Live character count for comments (optional)
    const charCount = document.getElementById('char-count');
    
    if (comments && charCount) {
        comments.addEventListener('input', function() {
            charCount.textContent = `${comments.value.length} characters`;
        });
    }
    
    form.addEventListener('input', validateForm);
    form.addEventListener('submit', function(event) {
        event.preventDefault();
        if (validateForm()) {
            renderSummary(destination.value, startDate.value, endDate.value, preferences.value, comments.value);
        }
    });

    function validateForm() {
        let valid = true;
        errorMessage.style.display = 'none';
        errorMessage.textContent = '';

        if (destination.value.trim() === '') {
            valid = false;
            errorMessage.style.display = 'block';
            errorMessage.textContent += 'Destination cannot be empty. ';
        }

        if (startDate.value === '' || endDate.value === '') {
            valid = false;
            errorMessage.style.display = 'block';
            errorMessage.textContent += 'Travel dates cannot be empty. ';
        }

        if (new Date(startDate.value) >= new Date(endDate.value)) {
            valid = false;
            errorMessage.style.display = 'block';
            errorMessage.textContent += 'Start date must be earlier than end date. ';
        }

        submitButton.disabled = !valid;
        return valid;
    }

    function renderSummary(destination, startDate, endDate, preferences, comments) {
        const existingSummaryContent = summarySection.querySelector(".summary-content");
        if(existingSummaryContent) {
            existingSummaryContent.remove();
        }

        const summaryContent = document.createElement("div");
        summaryContent.classList.add("summary-content");

        const destinationParagraph = document.createElement("p");
        destinationParagraph.textContent = `Destination: ${destination}`;
        summaryContent.appendChild(destinationParagraph);

        const startDateParagraph = document.createElement("p");
        startDateParagraph.textContent = `Start Date: ${startDate}`;
        summaryContent.appendChild(startDateParagraph);

        const endDateParagraph = document.createElement("p");
        endDateParagraph.textContent = `End Date: ${endDate}`;
        summaryContent.appendChild(endDateParagraph);

        const preferencesParagraph = document.createElement("p");
        preferencesParagraph.textContent = `Preferences: ${preferences}`;
        summaryContent.appendChild(preferencesParagraph);

        const commentsParagraph = document.createElement("p");
        commentsParagraph.textContent = `Comments: ${comments || "No comments provided."}`;
        summaryContent.appendChild(commentsParagraph);

        // Append the new summary content to the summary section
        summarySection.appendChild(summaryContent);
        summarySection.style.display = 'block';
        

    }
});
