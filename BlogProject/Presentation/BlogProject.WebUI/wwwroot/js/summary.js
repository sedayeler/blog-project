document.addEventListener("DOMContentLoaded", function () {
    const btn = document.getElementById("summarizeBtn");
    const box = document.getElementById("summaryBox");

    if (!btn || !box) return;

    btn.addEventListener("click", async function () {
        const postId = btn.getAttribute("data-post-id");
        box.classList.remove("d-none");
        box.textContent = "AI özet hazırlanıyor...";

        try {
            const response = await fetch('/Post/Summarize', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: postId })
            });

            if (!response.ok) {
                throw new Error("Sunucu hatası");
            }

            const data = await response.json();
            box.textContent = data.summary;
        } catch (error) {
            box.textContent = "AI özeti alınırken bir hata oluştu.";
            console.error(error);
        }
    });
});
