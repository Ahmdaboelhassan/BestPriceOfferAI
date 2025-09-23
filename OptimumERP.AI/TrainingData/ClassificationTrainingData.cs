using OptimumERP.AI.Models;

namespace OptimumERP.AI.TrainingData
{
    public static class ClassificationTrainingData
    {
        public static List<ColumnNameData> GetData = new List<ColumnNameData>
        {
            // ================= ITEM =================
            new ColumnNameData { ColumnText = "Item", Label = "Item" },
            new ColumnNameData { ColumnText = "Items", Label = "Item" },
            new ColumnNameData { ColumnText = "Product", Label = "Item" },
            new ColumnNameData { ColumnText = "Product Name", Label = "Item" },
            new ColumnNameData { ColumnText = "Article", Label = "Item" },
            new ColumnNameData { ColumnText = "Material", Label = "Item" },
            new ColumnNameData { ColumnText = "Goods", Label = "Item" },
            new ColumnNameData { ColumnText = "SKU", Label = "Item" },
            new ColumnNameData { ColumnText = "Barcode", Label = "Item" },
            new ColumnNameData { ColumnText = "Part Number", Label = "Item" },
            // Arabic
            new ColumnNameData { ColumnText = "الصنف", Label = "Item" },
            new ColumnNameData { ColumnText = "المنتج", Label = "Item" },
            new ColumnNameData { ColumnText = "اسم المنتج", Label = "Item" },
            new ColumnNameData { ColumnText = "المادة", Label = "Item" },
            new ColumnNameData { ColumnText = "البضاعة", Label = "Item" },
            new ColumnNameData { ColumnText = "كود الصنف", Label = "Item" },

            // ================= QUANTITY =================
            new ColumnNameData { ColumnText = "Quantity", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Qty", Label = "Quantity" },
            new ColumnNameData { ColumnText = "QTY", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Count", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Units", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Pieces", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Nos", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Amount", Label = "Quantity" },
            // Arabic
            new ColumnNameData { ColumnText = "الكمية", Label = "Quantity" },
            new ColumnNameData { ColumnText = "عدد", Label = "Quantity" },
            new ColumnNameData { ColumnText = "العدد", Label = "Quantity" },
            new ColumnNameData { ColumnText = "الوحدات", Label = "Quantity" },
            new ColumnNameData { ColumnText = "قطع", Label = "Quantity" },

            // ================= PRICE & TOTAL =================
            new ColumnNameData { ColumnText = "Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Unit Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Cost", Label = "Price" },
            new ColumnNameData { ColumnText = "Selling Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Net Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Base Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Retail Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Subtotal", Label = "Price" },
            new ColumnNameData { ColumnText = "Sub Total", Label = "Price" },
            new ColumnNameData { ColumnText = "Total", Label = "Price" },
            new ColumnNameData { ColumnText = "Grand Total", Label = "Price" },
            new ColumnNameData { ColumnText = "Final Total", Label = "Price" },
            new ColumnNameData { ColumnText = "Overall Total", Label = "Price" },
            new ColumnNameData { ColumnText = "Balance", Label = "Price" },
            new ColumnNameData { ColumnText = "Amount Due", Label = "Price" },
            new ColumnNameData { ColumnText = "Total Amount", Label = "Price" },
            // Arabic
            new ColumnNameData { ColumnText = "السعر", Label = "Price" },
            new ColumnNameData { ColumnText = "سعر الوحدة", Label = "Price" },
            new ColumnNameData { ColumnText = "التكلفة", Label = "Price" },
            new ColumnNameData { ColumnText = "سعر البيع", Label = "Price" },
            new ColumnNameData { ColumnText = "السعر الصافي", Label = "Price" },
            new ColumnNameData { ColumnText = "الإجمالي", Label = "Price" },
            new ColumnNameData { ColumnText = "الإجمالي الكلي", Label = "Price" },
            new ColumnNameData { ColumnText = "الإجمالي النهائي", Label = "Price" },
            new ColumnNameData { ColumnText = "المجموع", Label = "Price" },
            new ColumnNameData { ColumnText = "المبلغ المستحق", Label = "Price" },
            new ColumnNameData { ColumnText = "الصافي", Label = "Price" },

            // ================= DISCOUNT1 =================
            new ColumnNameData { ColumnText = "Discount", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Disc1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "D1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Discount1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Discount One", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Promo", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Offer", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Rebate", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Markdown", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Discount Ratio", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Discount %", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Discount Percentage", Label = "Discount1" },
            // Arabic
            new ColumnNameData { ColumnText = "الخصم", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خصم1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خصم أول", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خصم واحد", Label = "Discount1" },
            new ColumnNameData { ColumnText = "الخصم الأول", Label = "Discount1" },
            new ColumnNameData { ColumnText = "عرض", Label = "Discount1" },
            new ColumnNameData { ColumnText = "تخفيض", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خ1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خ 1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خ١", Label = "Discount1" },
            new ColumnNameData { ColumnText = "نسبة خصم", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خصم %", Label = "Discount1" },

            // ================= DISCOUNT2 =================
            new ColumnNameData { ColumnText = "Discount 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Disc2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "D2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Discount2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Discount Two", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Extra Discount", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Additional Discount", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Discount Ratio 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Discount %2", Label = "Discount2" },
            // Arabic
            new ColumnNameData { ColumnText = "خصم 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خصم إضافي", Label = "Discount2" },
            new ColumnNameData { ColumnText = "تخفيض إضافي", Label = "Discount2" },
            new ColumnNameData { ColumnText = "الخصم الثاني", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خصم ثاني", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خ2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خ 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خ٢", Label = "Discount2" },
            new ColumnNameData { ColumnText = "نسبة خصم 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خصم %٢", Label = "Discount2" },

            // ================= DISCOUNT3 =================
            new ColumnNameData { ColumnText = "Discount 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Disc3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "D3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Discount3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Discount Three", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Special Discount", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Final Discount", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Discount Ratio 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Discount %3", Label = "Discount3" },
            // Arabic
            new ColumnNameData { ColumnText = "خصم 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خصم نهائي", Label = "Discount3" },
            new ColumnNameData { ColumnText = "تخفيض خاص", Label = "Discount3" },
            new ColumnNameData { ColumnText = "الخصم الثالث", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خصم ثالث", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خ3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خ 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خ٣", Label = "Discount3" },
            new ColumnNameData { ColumnText = "نسبة خصم 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خصم %٣", Label = "Discount3" },

            };

    }
}
