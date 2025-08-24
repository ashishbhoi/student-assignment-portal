(function () {
    // Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
    // for details on configuring this project to bundle and minify static web assets.

    // Write your JavaScript code.

    // Immediately invoked function to set the theme on initial load to prevent FOUC
    (function () {
        if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
            document.documentElement.classList.add('dark');
        } else {
            document.documentElement.classList.remove('dark');
        }
    })();

    window.StudentPortal = {
        openCreateFileModal: function (virtualFolderId) {
            document.getElementById('modalVirtualFolderId').value = virtualFolderId;
            document.getElementById('createTextFileModal').classList.remove('hidden');
            document.getElementById('createTextFileModal').classList.add('flex');
        },

        closeCreateFileModal: function () {
            document.getElementById('createTextFileModal').classList.add('hidden');
            document.getElementById('createTextFileModal').classList.remove('flex');
        },

        selectAll: function (source, group) {
            const checkboxes = document.getElementsByClassName('student-checkbox-' + group);
            for (const checkbox of checkboxes) {
                checkbox.checked = source.checked;
            }
        },

        toggleTheme: function () {
            const themeToggleDarkIcon = document.getElementById('theme-toggle-dark-icon');
            const themeToggleLightIcon = document.getElementById('theme-toggle-light-icon');

            const isDarkMode = document.documentElement.classList.toggle('dark');
            localStorage.theme = isDarkMode ? 'dark' : 'light';

            themeToggleDarkIcon.classList.toggle('hidden');
            themeToggleLightIcon.classList.toggle('hidden');
        }
    };

    document.addEventListener('DOMContentLoaded', function () {

        // Logic for theme toggle
        const themeToggleBtn = document.getElementById('theme-toggle');
        const themeToggleDarkIcon = document.getElementById('theme-toggle-dark-icon');
        const themeToggleLightIcon = document.getElementById('theme-toggle-light-icon');

        function setInitialThemeIcon() {
            if (document.documentElement.classList.contains('dark')) {
                themeToggleLightIcon.classList.remove('hidden');
                themeToggleDarkIcon.classList.add('hidden');
            } else {
                themeToggleDarkIcon.classList.remove('hidden');
                themeToggleLightIcon.classList.add('hidden');
            }
        }

        setInitialThemeIcon();

        if (themeToggleBtn) {
            themeToggleBtn.addEventListener('click', StudentPortal.toggleTheme);
        }

        // Logic for CreateUser page
        const roleSelector = document.getElementById('roleSelector');
        if (roleSelector) {
            roleSelector.addEventListener('change', function () {
                const studentFields = document.getElementById('studentFields');
                if (this.value === 'Student') {
                    studentFields.classList.remove('hidden');
                } else {
                    studentFields.classList.add('hidden');
                }
            });
        }

        // Logic for StudentManagement page modals
        const promoteBtn = document.getElementById('promoteSelectedBtn');
        const cancelPromoteBtn = document.getElementById('cancelPromotionBtn');
        const promotionModal = document.getElementById('promotionModal');

        if (promoteBtn && promotionModal) {
            promoteBtn.addEventListener('click', function () {
                promotionModal.classList.remove('hidden');
            });
        }
        if (cancelPromoteBtn && promotionModal) {
            cancelPromoteBtn.addEventListener('click', function () {
                promotionModal.classList.add('hidden');
            });
        }

        const bulkImportBtn = document.getElementById('bulkImportBtn');
        const cancelBulkImportBtn = document.getElementById('cancelBulkImportBtn');
        const bulkImportModal = document.getElementById('bulkImportModal');

        if (bulkImportBtn && bulkImportModal) {
            bulkImportBtn.addEventListener('click', function () {
                bulkImportModal.classList.remove('hidden');
            });
        }
        if (cancelBulkImportBtn && bulkImportModal) {
            cancelBulkImportBtn.addEventListener('click', function () {
                bulkImportModal.classList.add('hidden');
            });
        }

        // Logic for select all checkbox
        const selectAllCheckboxes = document.querySelectorAll('.select-all-checkbox');
        selectAllCheckboxes.forEach(checkbox => {
            checkbox.addEventListener('change', function () {
                const group = this.getAttribute('data-group');
                StudentPortal.selectAll(this, group);
            });
        });

        // Logic for delete file form
        const deleteFileForms = document.querySelectorAll('.delete-file-form');
        deleteFileForms.forEach(form => {
            form.addEventListener('submit', function (e) {
                if (!confirm('Are you sure you want to delete this file?')) {
                    e.preventDefault();
                }
            });
        });

        // Logic for upload file form
        const uploadFileForms = document.querySelectorAll('.upload-file-form');
        uploadFileForms.forEach(form => {
            form.addEventListener('submit', function (e) {
                if (!confirm('If you have already submitted a file for this assignment, it will be replaced. Are you sure you want to continue?')) {
                    e.preventDefault();
                }
            });
        });

        // Logic for create file button
        const createFileBtns = document.querySelectorAll('.create-file-btn');
        createFileBtns.forEach(btn => {
            btn.addEventListener('click', function () {
                const assignmentId = this.getAttribute('data-assignment-id');
                StudentPortal.openCreateFileModal(assignmentId);
            });
        });

        // Logic for cancel create file button
        const cancelCreateFileBtn = document.getElementById('cancel-create-file');
        if (cancelCreateFileBtn) {
            cancelCreateFileBtn.addEventListener('click', StudentPortal.closeCreateFileModal);
        }

        // Logic for delete subject form
        const deleteSubjectForms = document.querySelectorAll('.delete-subject-form');
        deleteSubjectForms.forEach(form => {
            form.addEventListener('submit', function (e) {
                if (!confirm('Are you sure you want to delete this subject?')) {
                    e.preventDefault();
                }
            });
        });

        // Logic for delete public file form
        const deletePublicFileForms = document.querySelectorAll('.delete-public-file-form');
        deletePublicFileForms.forEach(form => {
            form.addEventListener('submit', function (e) {
                if (!confirm('Are you sure you want to delete this file?')) {
                    e.preventDefault();
                }
            });
        });
    });
})();
