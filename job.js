
document.addEventListener("DOMContentLoaded", () => {

   
    document.querySelectorAll('nav a').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const targetId = this.getAttribute('href').slice(1);
            const targetElement = document.getElementById(targetId);
            if (targetElement) {
                targetElement.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });

   
    document.querySelectorAll('.buttons a').forEach(button => {
        button.addEventListener('mouseover', () => {
            button.style.transform = 'scale(1.1)';
            button.style.boxShadow = '0 4px 10px rgba(0, 0, 0, 0.3)';
        });
        button.addEventListener('mouseout', () => {
            button.style.transform = 'scale(1)';
            button.style.boxShadow = 'none';
        });
    });

   
    const toggleThemeButton = document.createElement('button');
    toggleThemeButton.textContent = 'Toggle Theme';
    toggleThemeButton.style.position = 'fixed';
    toggleThemeButton.style.top = '20px';
    toggleThemeButton.style.right = '20px';
    toggleThemeButton.style.padding = '10px';
    toggleThemeButton.style.borderRadius = '5px';
    toggleThemeButton.style.border = 'none';
    toggleThemeButton.style.backgroundColor = '#4CAF50';
    toggleThemeButton.style.color = 'white';
    toggleThemeButton.style.cursor = 'pointer';
    document.body.appendChild(toggleThemeButton);

    toggleThemeButton.addEventListener('click', () => {
        document.body.classList.toggle('dark-theme');
        if (document.body.classList.contains('dark-theme')) {
            toggleThemeButton.textContent = 'Switch to Light Theme';
        } else {
            toggleThemeButton.textContent = 'Switch to Dark Theme';
        }
    });

    
    document.querySelectorAll('.job-form').forEach(form => {
        form.addEventListener('submit', (e) => {
            let isValid = true;
            form.querySelectorAll('input, select, textarea').forEach(input => {
                if (input.hasAttribute('required') && !input.value.trim()) {
                    isValid = false;
                    input.style.borderColor = 'red';
                    input.style.boxShadow = '0 0 5px rgba(255, 0, 0, 0.5)';
                } else {
                    input.style.borderColor = '#ccc';
                    input.style.boxShadow = 'none';
                }
            });

            if (!isValid) {
                e.preventDefault();
                alert('Please fill out all required fields.');
            }
        });
    });

   
    const viewButton = document.querySelector("a[href='view-jobs.html']");
    const postButton = document.querySelector("a[href='post-job.html']");

    viewButton.addEventListener("mouseover", () => {
        viewButton.style.transform = "scale(1.1)";
    });

    viewButton.addEventListener("mouseout", () => {
        viewButton.style.transform = "scale(1)";
    });

    postButton.addEventListener("mouseover", () => {
        postButton.style.transform = "scale(1.1)";
    });

    postButton.addEventListener("mouseout", () => {
        postButton.style.transform = "scale(1)";
    });

});
