using Inventory_API.Services;

namespace Inventory_API.Tools
{
    public class InvEnums
    {
        public enum OperationCode
        {
            Created,
            Edited,
            Deleted,
            Supply,
            OnBalance,
            Relocate,
            WriteOff
        }

        public enum Table
        {
            Equip,
            Room,
            Org,
            Type,
            Status,
            Accountability,
            CheckInfo,
            MOL,
        }

        public enum DocumentType
        {

        }

        public enum HistoryProperty
        {
            [StringValue("-")] None,
            [StringValue("Название")] Name,
            [StringValue("Инвентарный номер")] InvNum,
            [StringValue("Помещение")] Room,
            [StringValue("Тип")] Type,
            [StringValue("Статус")] Status,
            [StringValue("Подотчет")] Accountability,
            [StringValue("Заметка")] Note,
            [StringValue("Количество")] Count
        }

        public enum DepreciationGroups
        {
            [StringValue("Первая группа")] I,
            [StringValue("Вторая группа")] II,
            [StringValue("Третья группа")] III,
            [StringValue("Четвертая группа")] IV,
            [StringValue("Пятая группа")] V,
            [StringValue("Шестая группа")] VI,
            [StringValue("Седьмая группа")] VII,
            [StringValue("Восьмая группа")] VIII
        }
    }
}
