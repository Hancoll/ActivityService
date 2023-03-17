﻿namespace EventService.Application.Features.Images;

public class ImagesService
{
    private readonly List<Guid> _images = new() { Guid.NewGuid() };

    public Guid GetRandomImageId()
    {
        return _images.First();
    }

    public bool IsImageExists(Guid guid)
    {
        return _images.Contains(guid);
    }
}
