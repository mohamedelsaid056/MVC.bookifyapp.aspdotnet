$(document).ready(function () {
    $('#GovernorateId').on('change', function () {
        var governorateId = $(this).val();
        var areaList = $('#AreaId');

        areaList.empty();
        areaList.append('<option></option>');

        if (governorateId !== '') {
            $.ajax({
                url: '/Subscribers/GetAreas?governorateId=' + governorateId,
                success: function (areas) {
                    $.each(areas, function (i, area) {
                        var item = $('<option></option>').attr("value", area.value).text(area.text);
                        areaList.append(item);
                    });
                },
                error: function () {
                    showErrorMessage();
                }
            });
        }
    });
});




// this js native code

//************************************************************************ */
// document.addEventListener('DOMContentLoaded', function () {
//     var governorateSelect = document.getElementById('GovernorateId');
//     var areaList = document.getElementById('AreaId');

//     governorateSelect.addEventListener('change', function () {
//         var governorateId = this.value;

//         areaList.innerHTML = '';  // Clear the area list
//         var emptyOption = document.createElement('option');
//         areaList.appendChild(emptyOption);  // Append an empty option

//         if (governorateId !== '') {
//             fetch('/Subscribers/GetAreas?governorateId=' + governorateId)
//                 .then(response => response.json())
//                 .then(areas => {
//                     areas.forEach(area => {
//                         var option = document.createElement('option');
//                         option.value = area.value;
//                         option.textContent = area.text;
//                         areaList.appendChild(option);
//                     });
//                 })
//                 .catch(error => {
//                     showErrorMessage();  // Handle error
//                 });
//         }
//     });
// });
