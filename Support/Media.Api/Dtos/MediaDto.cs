namespace Media.Api.Dtos
{
    public class MediaDto
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public long SizeInKiloBytes => Size % 1024 == 0 ? Size / 1024 : Size / 1024 + 1;
        public long SizeInMegaBytes => SizeInKiloBytes % 1024 == 0 ? SizeInKiloBytes / 1024 : SizeInKiloBytes / 1024 + 1;
    }
}
