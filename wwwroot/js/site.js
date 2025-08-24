// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function openCreateFileModal(virtualFolderId) {
    document.getElementById('modalVirtualFolderId').value = virtualFolderId;
    document.getElementById('createTextFileModal').classList.remove('hidden');
    document.getElementById('createTextFileModal').classList.add('flex');
}

function closeCreateFileModal() {
    document.getElementById('createTextFileModal').classList.add('hidden');
    document.getElementById('createTextFileModal').classList.remove('flex');
}

function selectAll(source, group) {
    const checkboxes = document.getElementsByClassName('student-checkbox-' + group);
    for (let i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
    }
}

document.addEventListener('DOMContentLoaded', function () {
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
});