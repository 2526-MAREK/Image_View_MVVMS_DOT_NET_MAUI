using Image_View_V1._0.Services;
using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Image_View_V1._0.Services
{
    public class ImageDataBaseService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "DataBaseForImageProcessed.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<ImageToProcess>();
            Debug.WriteLine("Utworzono Tabele!!!");
        }

        public async Task AddImageAfterProcessToDataBase(ImageToProcess imageToProcess)
        {
            await Init();

            var id = await db.InsertAsync(imageToProcess);
        }

        public async Task RemoveImageAfterProcessFromDataBase(int id)
        {

            await Init();

            await db.DeleteAsync<ImageToProcess>(id);
        }

        public async Task<IEnumerable<ImageToProcess>> GetAllImageAfterProcessFromDataBase()
        {
            await Init();

            var imageToProcess = await db.Table<ImageToProcess>().ToListAsync();
            return imageToProcess;
        }

        public async Task<ImageToProcess> GetImageAfterProcessFromDataBase(int id)
        {
            await Init();

            var imageToProcess = await db.Table<ImageToProcess>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return imageToProcess;
        }

    }
}
